namespace Api.Shared;

using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema("Output of the get recording request")]
public record RecordingGetResponseV1
{
    [SwaggerSchema(Description = "The recording definition", Nullable = true)]
    public RecordingV1? Recording { get; init; }
}