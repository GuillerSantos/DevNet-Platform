using AutoMapper;
using DevNet.Application.Abstractions;
using DevNet.Application.Abstractions.Persistence;
using DevNet.Application.Common;
using DevNet.Application.Common.Responses;
using DevNet.Application.DTO.Auth;
using MediatR;

namespace DevNet.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IApplicationUserRepository applicationUserRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IJwtService jwtService) : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
    {
        #region Public Methods

        public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var storedToken = await refreshTokenRepository.GetByRefreshTokenAsync(request.RefreshToken);

                if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
                    return Result<AuthResponse>.Failure("Invalid or expired refresh token.");
                // GlobalExceptionMiddleware: throw new UnauthorizedException("Invalid or expired refresh token.");

                var user = await applicationUserRepository.GetByEmailAsync(storedToken.Email);

                if (user == null)
                    return Result<AuthResponse>.Failure("User not found.");
                // GlobalExceptionMiddleware: throw new NotFoundException("User not found.");

                await unitOfWork.BeginTransactionAsync(cancellationToken);

                await refreshTokenRepository.RevokeRefreshTokenAsync(request.RefreshToken);

                var tokenUser = mapper.Map<TokenUserDto>(user);
                var accessToken = jwtService.GenerateAccessToken(tokenUser);
                var newRefreshToken = jwtService.GenerateRefreshToken();

                var refreshTokenEntity = new Domain.Entities.RefreshTokens
                {
                    RefreshToken = newRefreshToken,
                    Email = user.Email,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    ApplicationUserId = user.Id
                };

                await refreshTokenRepository.AddAsync(refreshTokenEntity);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                };

                return Result<AuthResponse>.Success(response);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollBackAsync(cancellationToken);
                return Result<AuthResponse>.Failure($"An Error Occured: {ex.Message}");
                // GlobalExceptionMiddleware: throw;
            }
        }

        #endregion Public Methods
    }
}