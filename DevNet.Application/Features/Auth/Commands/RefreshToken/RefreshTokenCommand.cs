using DevNet.Application.Common;
using DevNet.Application.Common.Responses;
using MediatR;

namespace DevNet.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Result<AuthResponse>>
    {
        #region Properties

        public string RefreshToken { get; set; } = string.Empty;

        #endregion Properties
    }
}