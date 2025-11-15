namespace DevNet.Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        #region Public Methods

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        ValueTask DisposeAsync();

        Task RollBackAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Public Methods
    }
}