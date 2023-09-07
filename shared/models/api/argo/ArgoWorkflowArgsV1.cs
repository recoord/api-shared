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
    public string Namespace { get; set; } = "processing-argo";

    [SwaggerSchemaExample("WorkflowTemplate")]
    public string ResourceKind { get; set; } = "WorkflowTemplate";

    [SwaggerSchemaExample("stacked-rendering")]
    public string ResourceName { get; set; } = "";

    public SubmitOptions SubmitOptions { get; set; } = new SubmitOptions();
}