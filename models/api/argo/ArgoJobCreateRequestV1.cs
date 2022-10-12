public record ArgoJobCreateRequestV1
{
    public string WorkflowName { get; init; } = "";
    public List<string> JobInputs { get; init; } = new List<string>();
    public double JobTimeoutMinutes { get; init; } = 60 * 24;
}