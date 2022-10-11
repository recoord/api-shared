using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema(Description = @$"Arguments to be use for the specifiec job kind in the specified job system. <br/>
These are the following schemas to use depending on what job system you are using: <br/>
            <ul>
            <li>[Argo](#/model-{nameof(ArgoWorkflowArgsV1)})</li>
            </ul>")]
public record JobSystemArgsV1
{
    [JsonExtensionData]
    public Dictionary<string, object> additionalJSON { get; init; } = new Dictionary<string, object>();
}