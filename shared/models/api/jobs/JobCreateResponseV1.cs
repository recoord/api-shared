namespace Api.Shared;

using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema("Output of the create job request")]
public record JobCreateResponseV1
{
    [SwaggerSchema(Description = "Info on the created job", Nullable = true)]
    public JobV1? Job { get; init; }
}