public record AwsCredentialsV1
{
    public string AccessKeyId { get; init; } = "";
    public string SecretAccessKey { get; init; } = "";
    public string SessionToken { get; init; } = "";
}