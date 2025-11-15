namespace DevNet.Application.DTO.Auth
{
    public class LoginRequest
    {
        #region Properties

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}