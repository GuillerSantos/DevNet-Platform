using DevNet.Application.Abstractions.Persistence;
using DevNet.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DevNet.Persistence.Repositories
{
    public class AsyncBaseRepository<T> : IAsyncBaseRepository<T> where T : class
    {
        #region Fields

        public readonly AppDbContext dbContext;

        #endregion Fields

        #region Public Constructors

        public AsyncBaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<T?> AddAsync(T entity)
        {
            var result = await dbContext.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbContext.AddRangeAsync(entities);
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<T?> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
                dbContext.Set<T>().Remove(entity);

            return entity;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        #endregion Public Methods
    }
}