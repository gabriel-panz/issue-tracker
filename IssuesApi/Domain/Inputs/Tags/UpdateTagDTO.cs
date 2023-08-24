using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Domain.Inputs.Tags;

public class UpdateTagDTO
{
    [Required]
    public string Name { get; set; } = null!;
}
