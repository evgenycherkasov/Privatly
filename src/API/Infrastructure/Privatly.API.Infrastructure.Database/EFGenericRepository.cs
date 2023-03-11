using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Privatly.API.Domain.Entities;
using Privatly.API.Domain.Interfaces;

namespace Privatly.API.Infrastructure.Database;

// ReSharper disable once InconsistentNaming
public abstract class EFGenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected EFGenericRepository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }

    protected IQueryable<TEntity?> GetQueryable(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null, int? take = null)
    {
        IQueryable<TEntity?> query = DbSet;

        if (filter is not null)
        {
            query = query.Where(filter!);
        }

        if (orderBy is not null)
        {
            query = orderBy(query!);
        }

        if (includes is {Length: > 0})
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include!));
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public async Task<TEntity> AddAsync(TEntity? entity)
    {
        if (entity != null) 
            return (await DbSet.AddAsync(entity)).Entity;

        throw new ArgumentNullException(nameof(entity));
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        return await GetQueryable(filter).CountAsync();
    }


    public void Delete(params TEntity?[] entity)
    {
        DbSet.RemoveRange(entity!);
    }

    public async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await GetQueryable().ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(object? id)
    {
        if (id == null)
        {
            return null;
        }

        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, object>>[]? includes = null, int? skip = null, int? take = null)
    {
        return await GetQueryable(filter, orderBy, includes, skip, take).ToListAsync();
    }

    public void Update(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }
}