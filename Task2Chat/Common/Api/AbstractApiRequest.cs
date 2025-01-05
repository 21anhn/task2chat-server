namespace Task2Chat.Common.Api
{
    public abstract class AbstractApiRequest
    {
        public string FuncId { get; set; }
        public bool IsOnlyValidation { get; set; }
    }
}
