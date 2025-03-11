using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces.ORM;

namespace Domain.Interfaces.GenericRepository
{
    public interface IGenericRepository<T> where T : class,IEntity,new()
    {
        Task AddAsync(T entity);
        Task UpdateAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression <Func<T, bool>> predicate);
        Task SaveChangeAsync();
    }
}