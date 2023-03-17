using System.Linq.Expressions;
using Privatly.API.Domain.Entities;

namespace Privatly.API.Domain.Interfaces;

public interface IGenericRepository<TEntity> 
    where TEntity : BaseEntity
{
    Task<TEntity?> GetAsync(object? id);
    
    Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null, int? take = null);
    
    Task<IReadOnlyCollection<TEntity>> GetAllAsync();
    
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
    
    Task<TEntity> AddAsync(TEntity? entity);
    
    void Update(TEntity entity);
    
    void Delete(params TEntity?[] entity);
}