namespace Api.Shared;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

[ExcludeFromCodeCoverage]
[SwaggerSchema(Description = "The argo workflow arguments")]
public class ArgoWorkflowArgsV1
{
    [SwaggerSchemaExample("processing-argo")]
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    [SwaggerSchemaExample("WorkflowTemplate")]
    public string? resourceKind { get; set; }

    [SwaggerSchemaExample("stacked-rendering")]
    public string? resourceName { get; set; }

    public SubmitOptions? submitOptions { get; set; }
}