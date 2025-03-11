using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces.GenericRepository;
using Domain.Interfaces.ORM;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        public readonly TrinkappContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(TrinkappContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            var entity = predicate == null
            ? await _dbSet.ToListAsync()
            : await _dbSet.Where(predicate).ToListAsync();
            return entity;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbSet.FirstOrDefault(predicate);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                return entity;
            }
            return null;
        }

        public async Task SaveChangeAsync()
        {
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Update(entity);
            }
        }
    }
}