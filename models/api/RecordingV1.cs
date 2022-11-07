using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema(Description = "The recording definition")]
public record RecordingV1
{
    [SwaggerSchema(Description = "Unique id of the recording")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    public Guid RecordingId { get; init; }

    [SwaggerSchema(Description = "Url for video.ts file")]
    public string? VideoTsUrl { get; init; }

    [SwaggerSchema(Description = "Url for upload.veo file")]
    public string? UploadVeoUrl { get; init; }
}