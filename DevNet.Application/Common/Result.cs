namespace DevNet.Application.Common
{
    public class Result<T>
    {
        #region Properties

        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public T? Value { get; set; }

        #endregion Properties

        #region Public Methods

        public static Result<T> Success(T value)
        {
            return new()
            {
                IsSuccess = true,
                Value = value
            };
        }

        public static Result<T> Failure(string error)
        {
            return new()
            {
                IsSuccess = false,
                Error = error
            };
        }

        #endregion Public Methods
    }
}