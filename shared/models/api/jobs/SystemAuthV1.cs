namespace Api.Shared;

using System.ComponentModel.DataAnnotations;

public record SystemAuthV1
{
    [SwaggerSchemaExample("Bearer")]
    public string Header { get; init; } = "Bearer";

    [Required]
    public string Token { get; init; } = "";
}