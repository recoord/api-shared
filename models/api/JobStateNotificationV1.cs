using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

public record JobStateNotificationV1
{
    [SwaggerSchema(Description = "Job id")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    [Required]
    [NotEmptyGuid]
    public Guid JobId { get; init; }

    [Required]
    [ValidEnum(AllowZeroValues = false)]
    public JobStateV1 JobState { get; init; }

    [SwaggerSchema(Description = "Timestamp in UTC format where job state changed happened in jobs service")]
    [SwaggerSchemaExample("2022-08-10T14:14:41Z")]
    [Required]
    public DateTime StateChangedAt { get; init; }

    public int ExitCode { get; init; }
}