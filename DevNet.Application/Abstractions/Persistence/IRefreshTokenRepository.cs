using DevNet.Application.Features.RefreshTokens.Commands.GetByEmail;
using DevNet.Domain.Entities;

namespace DevNet.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepository : IAsyncBaseRepository<RefreshTokens>
    {
        #region Public Methods

        Task RevokeRefreshTokenAsync(string refreshToken);

        Task RevokeAllUserRefreshTokenAsync(string email);

        Task<RefreshTokenDto?> GetByRefreshTokenAsync(string refreshToken);

        Task<RefreshTokenDto?> GetByEmailAsync(string email);

        #endregion Public Methods
    }
}