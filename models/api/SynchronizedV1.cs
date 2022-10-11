using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

public record SynchronizedV1
{
    [Required]
    [NotEmptyGuid]
    public SynchronizePlanV1 Plan { get; init; }

    [SwaggerSchema(Description = "Tag keys as used under 'Tags'")]
    [Required]
    public List<string> OnTags { get; init; } = new List<string>();
}