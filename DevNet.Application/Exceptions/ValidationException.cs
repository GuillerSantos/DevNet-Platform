namespace DevNet.Application.Exceptions
{
    public class ValidationException : Exception
    {
        #region Public Constructors

        public ValidationException(string message, IEnumerable<string>? errors = null)
            : base(message)
        {
            ValidationErrors = errors?.ToList() ?? new List<string>();
        }

        #endregion Public Constructors

        #region Properties

        public List<string> ValidationErrors { get; set; }

        #endregion Properties
    }
}