using DevNet.Application.Abstractions;

namespace DevNet.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        #region Public Methods

        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));

        public bool VerifyPassword(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        #endregion Public Methods
    }
}