using DevNet.Domain.Enums;

namespace DevNet.Application.Common.Responses
{
    public class AuthResponse
    {
        #region Properties

        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public Roles Role { get; set; }

        #endregion Properties
    }
}