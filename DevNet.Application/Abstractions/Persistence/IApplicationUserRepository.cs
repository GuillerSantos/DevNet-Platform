using DevNet.Domain.Entities;

namespace DevNet.Application.Abstractions.Persistence
{
    public interface IApplicationUserRepository : IAsyncBaseRepository<ApplicationUser>
    {
        #region Public Methods

        Task<ApplicationUser?> GetByEmailAsync(string email);

        Task<ApplicationUser?> GetByFirstNameAsync(string firstName);

        Task<bool> EmailExistsAsync(string email);

        #endregion Public Methods
    }
}