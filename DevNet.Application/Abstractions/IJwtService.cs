using DevNet.Application.DTO.Auth;
using System.Security.Claims;

namespace DevNet.Application.Abstractions
{
    public interface IJwtService
    {
        #region Public Methods

        string GenerateAccessToken(TokenUserDto user);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);

        string GenerateRefreshToken();

        bool IsTokenValid(string token);

        #endregion Public Methods
    }
}