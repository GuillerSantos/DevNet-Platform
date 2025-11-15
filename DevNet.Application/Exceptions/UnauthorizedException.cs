namespace DevNet.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        #region Public Constructors

        public UnauthorizedException(string message) : base(message)
        {
        }

        #endregion Public Constructors
    }
}