using System.ComponentModel;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

// Lists all the statuses we want to expose in the api.
[SwaggerSchema("Job states. A job can only be in one of these states.")]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobStateV1
{
    Unspecified = 0,
    Scheduled = 1,
    Running = 2,
    Aborted = 3,
    Failed = 4,
    Completed = 5,
    TimedOut = 6
}