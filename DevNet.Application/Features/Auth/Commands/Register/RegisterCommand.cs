using DevNet.Application.Common;
using DevNet.Application.Common.Responses;
using DevNet.Domain.Enums;
using MediatR;

namespace DevNet.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<Result<AuthResponse>>
    {
        #region Properties

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public Roles Role { get; set; }

        #endregion Properties
    }
}