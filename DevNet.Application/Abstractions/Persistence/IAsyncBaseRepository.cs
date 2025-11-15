namespace DevNet.Application.Abstractions.Persistence
{
    public interface IAsyncBaseRepository<T>
    {
        #region Public Methods

        Task<IEnumerable<T>> ListAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<T?> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<T?> DeleteByIdAsync(Guid id);

        #endregion Public Methods
    }
}