public record SqsMessageHandlerResponse
{
    public int Retries { get; set; }
    public ESqsMessageHandlerResult Result { get; set; }
}