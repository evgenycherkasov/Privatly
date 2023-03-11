using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Privatly.API.Domain.Interfaces;
#pragma warning disable CS8625

namespace Privatly.API.Infrastructure.PostgreSQL;

public class UnitOfWork : IUnitOfWork
{
    private DbContext _dbContext;

    public UnitOfWork(PostgreDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitAsync()
    {
#if DEBUG
        var addedCount = _dbContext.ChangeTracker.Entries().Count( x => x.State == EntityState.Added );
        var updatedCount = _dbContext.ChangeTracker.Entries().Count( x => x.State == EntityState.Modified );
        var deletedCount = _dbContext.ChangeTracker.Entries().Count( x => x.State == EntityState.Deleted );

        Console.WriteLine( $"Added entities: {addedCount}" );
        Console.WriteLine( $"Updated entities: {updatedCount}" );
        Console.WriteLine( $"Deleted entities: {deletedCount}" );

        Debug.WriteLine( $"Added entities: {addedCount}" );
        Debug.WriteLine( $"Updated entities: {updatedCount}" );
        Debug.WriteLine( $"Deleted entities: {deletedCount}" );

#endif
        return await _dbContext.SaveChangesAsync();
    }

    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
    public void Dispose()
    {
        if ( _dbContext is not null )
        {
            _dbContext.Dispose();
            _dbContext = null;
        }
        GC.SuppressFinalize( this );
    }
}