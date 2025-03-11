using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Domain.Interfaces.GenericRepository;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories.GenericRepository;
using Infrastructure.Persistence.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrinkappContext _context;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories;
        public UnitOfWork(TrinkappContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }
        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            BeginTransaction();

            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw new TransactionException("An error occurred while committing the transaction.", ex);
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public void Dispose()
        {
            DisposeTransaction();
            _context.Dispose();
        }

        public void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction must be started before saving changes.");
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        IGenericRepository<T> IUnitOfWork.GetRepository<T>()
        {
            
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new GenericRepository<T>(_context);
            }
            return (IGenericRepository<T>)_repositories[typeof(T)];
        }
    }
}