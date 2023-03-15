using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema(Description = "The recording definition")]
public record RecordingV1
{
    [SwaggerSchema(Description = "Unique id of the recording")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    public Guid RecordingId { get; init; }

    [SwaggerSchema(Description = "Id of the camera that uploaded this recording")]
    [SwaggerSchemaExample("5ddf4520-2472-4666-973e-9a691850152a")]
    public Guid? CameraId { get; init; }

    [SwaggerSchema(Description = "S3 uri for video.ts file")]
    [SwaggerSchemaExample("s3://veo-stag-content/7848738c-2e16-4220-b0d8-43257f8dea38/video.ts")]
    public string? VideoTsS3Uri { get; init; }

    [SwaggerSchema(Description = "Object url for video.ts file")]
    [SwaggerSchemaExample("https://veo-stag-content.s3.eu-west-1.amazonaws.com/7848738c-2e16-4220-b0d8-43257f8dea38/video.ts")]
    public string? VideoTsUrl { get; init; }

    [SwaggerSchema(Description = "S3 uri for upload.veo file")]
    [SwaggerSchemaExample("s3://veo-stag-content/7848738c-2e16-4220-b0d8-43257f8dea38/upload.veo")]
    public string? UploadVeoS3Uri { get; init; }

    [SwaggerSchema(Description = "Object url for upload.veo file")]
    [SwaggerSchemaExample("s3://veo-stag-content/7848738c-2e16-4220-b0d8-43257f8dea38/upload.veo")]
    public string? UploadVeoUrl { get; init; }
}