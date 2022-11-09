using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema(Description = "Job create request")]
public record JobCreateRequestV1
{
    [Required]
    [SwaggerSchemaExample("argo")]
    [MinLength(1, ErrorMessage = "The JobSystemKind must be {1} characters long.")]
    public string JobSystemKind { get; init; } = "";

    [Required]
    [SwaggerSchemaExample("/workflows/processing-argo/submit")]
    public string JobSystemCreateUrlPath { get; init; } = "";

    [SwaggerSchemaExample("/workflows/processing-argo/{0}/stop")]
    public string JobSystemAbortUrlPath { get; init; } = "";

    [Range(0.1, 60 * 24, ErrorMessage = "Job timeout must be between {1} and {2}.")]
    public double JobTimeoutMinutes { get; init; } = 60 * 24;

    [SwaggerSchemaExample("stackedRendering")]
    public string JobKind { get; init; } = "noKind";

    //public SystemAuthV1? SystemAuth { get; init; }

    public JobSystemArgsV1? JobSystemArgs { get; init; }

    public TagsV1? Tags { get; init; }

    public NotificationV1? Notifications { get; init; }

    public SynchronizedV1? Synchronized { get; init; }
}
