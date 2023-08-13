using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Domain.Inputs.Tags;

public class CreateTagDTO
{
    [Required]
    public string Name { get; set; } = null!;
}
