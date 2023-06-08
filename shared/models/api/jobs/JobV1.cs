namespace Api.Shared;

using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema(Description = "The job definition")]
public record JobV1
{
    [SwaggerSchema(Description = "Unique id of the job (generated by the jobs API)")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    public Guid JobId { get; init; }

    [SwaggerSchema(Description = "Unique id of the job in jobs system (generated by the job system)")]
    public string JobSystemId { get; init; } = "";

    [SwaggerSchema(Description = "Type of job. For example stacked rendering job.")]
    public string JobKind { get; init; } = "";

    [SwaggerSchema(Description = "Current state of the job")]
    public JobStateV1 JobState { get; init; }

    [SwaggerSchema(Description = "Timestamp at which the job was requested")]
    public DateTime RequestedAt { get; init; }

    [SwaggerSchema(Description = "Timestamp at which the job started running on the job system")]
    public DateTime JobStartedAt { get; init; }

    [SwaggerSchema(Description = "Timestamp at which the job ended (caused by failing, aborted, completed or timed out)")]
    public DateTime JobEndedAt { get; init; }

    [SwaggerSchema(Description = "Timestamp at which the job finished processing (set on arrival in job state change event in jobs service)")]
    public DateTime ProcessedAt { get; init; }

    [SwaggerSchema(Description = "Exit code of the job")]
    public int ExitCode { get; init; }
}