namespace DevNet.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        #region Public Constructors

        public NotFoundException(string message) : base(message)
        {
        }

        #endregion Public Constructors
    }
}