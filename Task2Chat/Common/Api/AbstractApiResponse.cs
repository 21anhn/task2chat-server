namespace Task2Chat.Common.Api
{
    public abstract class AbstractApiResponse<T> where T : class
    {
        public string? MessageId { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; } = false;
        public abstract T? Response { get; set; }
        public List<DetailError>? DetailErrorList { get; set; }
    }
}
