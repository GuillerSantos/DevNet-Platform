using DevNet.Application.Abstractions.Persistence;
using DevNet.Application.Features.RefreshTokens.Commands.GetByEmail;
using DevNet.Domain.Entities;
using DevNet.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DevNet.Persistence.Repositories
{
    public class RefreshTokenRepository : AsyncBaseRepository<RefreshTokens>, IRefreshTokenRepository
    {
        #region Public Constructors

        public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<RefreshTokenDto?> GetByEmailAsync(string email)
        {
            return await dbContext.Set<RefreshTokens>()
                .Where(rt => rt.Email.ToLower() == email.ToLower() && !rt.IsRevoked)
                .Select(rt => new RefreshTokenDto(
                    rt.Id,
                    rt.RefreshToken,
                    rt.Email,
                    rt.ExpiresAt,
                    rt.IsRevoked,
                    rt.ApplicationUserId,
                    rt.FirstName,
                    rt.LastName
                ))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshTokenDto?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await dbContext.Set<RefreshTokens>()
                .Where(rt => rt.RefreshToken == refreshToken && !rt.IsRevoked)
                .Select(rt => new RefreshTokenDto(
                    rt.Id,
                    rt.RefreshToken,
                    rt.Email,
                    rt.ExpiresAt,
                    rt.IsRevoked,
                    rt.ApplicationUserId,
                    rt.FirstName,
                    rt.LastName
                ))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task RevokeAllUserRefreshTokenAsync(string email)
        {
            var tokens = await dbContext.Set<RefreshTokens>()
                 .Where(rt => rt.Email.ToLower() == email.ToLower() && !rt.IsRevoked)
                 .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.UpdatedAt = DateTime.UtcNow;
            }

            dbContext.Set<RefreshTokens>().UpdateRange(tokens);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var _refreshToken = await dbContext.Set<RefreshTokens>()
                .FirstOrDefaultAsync(rt => rt.RefreshToken == refreshToken);

            if (_refreshToken != null)
            {
                _refreshToken.IsRevoked = true;
                _refreshToken.UpdatedAt = DateTime.UtcNow;
                dbContext.Set<RefreshTokens>().Update(_refreshToken);
            }
        }

        #endregion Public Methods
    }
}