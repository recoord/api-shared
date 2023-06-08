namespace Api.Shared;

public record ArgoJobCreateRequestV1
{
    public string WorkflowName { get; init; } = "";
    public List<string> JobInputs { get; init; } = new List<string>();
    public double JobTimeoutMinutes { get; init; } = 60 * 24;
    public TagsV1? Tags { get; init; }
    public NotificationV1? Notifications { get; init; }
    public SynchronizedV1? Synchronized { get; init; }
    public AwsCredentialsV1? AwsCredentials { get; init; }
}