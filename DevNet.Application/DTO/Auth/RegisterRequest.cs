using DevNet.Domain.Enums;

namespace DevNet.Application.DTO.Auth
{
    public class RegisterRequest
    {
        #region Properties

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public Roles Role { get; set; }

        #endregion Properties
    }
}