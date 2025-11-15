namespace DevNet.Application.Common.Responses
{
    public class BaseResponse<T>
    {
        #region Public Constructors

        public BaseResponse() => Success = true;

        public BaseResponse(T data, string message = "")
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public BaseResponse(string message, bool success = false)
        {
            Success = success;
            Message = message;
        }

        #endregion Public Constructors

        #region Properties

        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public List<string>? ValidationErrors { get; set; }
        public T? Data { get; set; }

        #endregion Properties
    }
}