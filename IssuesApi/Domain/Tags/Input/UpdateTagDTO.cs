using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Domain.Inputs;

public class UpdateTagDTO
{
    [Required]
    public string Name { get; set; } = null!;
}
