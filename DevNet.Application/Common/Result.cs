namespace DevNet.Application.Common
{
    public class Result<T>
    {
        #region Private Constructors

        private Result()
        { }

        #endregion Private Constructors

        #region Properties

        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }
        public T? Value { get; private set; }

        #endregion Properties

        #region Public Methods

        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };

        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };

        #endregion Public Methods
    }
}