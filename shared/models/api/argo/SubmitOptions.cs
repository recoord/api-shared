namespace Api.Shared;

using System.Diagnostics.CodeAnalysis;
using Swashbuckle.AspNetCore.Annotations;

[ExcludeFromCodeCoverage]
public class SubmitOptions
{
    [SwaggerSchema(Description = "Parameters needed for the workflow. Jobs API will append job id (with 'job-api-id' key) for Argo to use to send back job state updates.")]
    [SwaggerSchemaExample("tsvideo-s3-link=https://c.veocdn.com/e9e1fb11-98c2-4dd8-ba16-4cdcfc5b283a/panorama/video.ts")]
    public IList<string> Parameters { get; set; } = new List<string>();
}