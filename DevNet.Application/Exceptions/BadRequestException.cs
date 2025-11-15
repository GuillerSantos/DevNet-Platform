namespace DevNet.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        #region Public Constructors

        public BadRequestException(string message) : base(message)
        {
        }

        #endregion Public Constructors
    }
}