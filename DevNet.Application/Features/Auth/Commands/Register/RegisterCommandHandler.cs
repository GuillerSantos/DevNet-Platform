using AutoMapper;
using DevNet.Application.Abstractions;
using DevNet.Application.Abstractions.Persistence;
using DevNet.Application.Common;
using DevNet.Application.Common.Responses;
using DevNet.Application.DTO.Auth;
using DevNet.Domain.Entities;
using MediatR;

namespace DevNet.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(IApplicationUserRepository applicationUserRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IPasswordHasherService passwordHasherService,
        IMapper mapper) : IRequestHandler<RegisterCommand, Result<AuthResponse>>
    {
        #region Public Methods

        public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await applicationUserRepository.EmailExistsAsync(request.Email))
                    return Result<AuthResponse>.Failure("Email already exists.");
                // GlobalExceptionMiddleware: throw new BadRequestException("Email already exists.");

                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var user = new ApplicationUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PasswordHash = passwordHasherService.HashPassword(request.Password),
                    Role = request.Role
                };

                await applicationUserRepository.AddAsync(user);

                var tokenUser = mapper.Map<TokenUserDto>(user);
                var accessToken = jwtService.GenerateAccessToken(tokenUser);
                var refreshToken = jwtService.GenerateRefreshToken();

                var refreshTokenEntity = new Domain.Entities.RefreshTokens
                {
                    RefreshToken = refreshToken,
                    Email = user.Email,
                    JoinedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    ApplicationUserId = user.Id
                };

                await refreshTokenRepository.AddAsync(refreshTokenEntity);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Email = user.Email,
                    Role = user.Role
                };

                return Result<AuthResponse>.Success(response);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollBackAsync(cancellationToken);
                return Result<AuthResponse>.Failure($"Registration failed: {ex.Message}");
                // GlobalExceptionMiddleware: throw;
            }
        }

        #endregion Public Methods
    }
}