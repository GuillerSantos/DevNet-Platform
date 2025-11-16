namespace DevNet.Application.Common
{
    public abstract class ResultBase
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
    }
}
