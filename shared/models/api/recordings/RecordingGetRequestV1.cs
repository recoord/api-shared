namespace Api.Shared;

using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

public record RecordingGetRequestV1
{
    [SwaggerSchema(Description = "Unique id of the recording")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    [Required]
    public Guid RecordingId { get; init; }
}