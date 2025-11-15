using DevNet.Application.Contracts.Persistence;
using DevNet.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevNet.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        #region Fields

        private readonly AppDbContext dbContext;
        private IDbContextTransaction transaction;

        #endregion Fields

        #region Public Constructors

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (transaction != null)
                return;

            transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        // Garbage collection implementation to clean up resources
        public async ValueTask DisposeAsync()
        {
            if (transaction != null)
                await transaction.DisposeAsync();

            await dbContext.DisposeAsync();
        }

        public async Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync(cancellationToken);
                await transaction.DisposeAsync();
                transaction = null;
            }
            else
            {
                // No trancation to roll back, so we just clear the tracked changes
                foreach (var entry in dbContext.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;

                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await dbContext.SaveChangesAsync(cancellationToken);

            if (transaction != null)
                await transaction.CommitAsync(cancellationToken);

            return result;
        }

        #endregion Public Methods
    }
}