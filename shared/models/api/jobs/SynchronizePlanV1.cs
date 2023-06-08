namespace Api.Shared;

using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema("Job states. A job can only be in one of these states.")]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SynchronizePlanV1
{
    Unspecified = 0,
    OnlyOneAllowed = 1,
    AlwaysRunLatest = 2
}