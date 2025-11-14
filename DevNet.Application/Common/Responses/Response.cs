namespace DevNet.Application.Common.Responses
{
    public sealed record Response
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = "";
        public string Message { get; set; } = "";
        public object? Data { get; set; }
        public Dictionary<string, string[]>? ValidationError { get; init; }
    }
}