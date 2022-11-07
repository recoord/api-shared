using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema(Description = "Recording enrichment add request")]
public record RecordingEnrichmentAddRequestV1
{
    [SwaggerSchema(Description = "Recording id")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    [Required]
    [NotEmptyGuid]
    public Guid RecordingId { get; init; }

    [Required]
    [MinLength(1, ErrorMessage = "The enrichment name field must not be empty")]
    public string EnrichmentName { get; init; } = "";

    [Required]
    [MinLength(1, ErrorMessage = "Url for recording enrichment")]
    public string Url { get; set; } = "";
}