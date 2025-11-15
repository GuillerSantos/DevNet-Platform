namespace DevNet.Application.Abstractions
{
    public interface IPasswordHasherService
    {
        #region Public Methods

        string HashPassword(string password);

        bool VerifyPassword(string password, string hashedPassword);

        #endregion Public Methods
    }
}