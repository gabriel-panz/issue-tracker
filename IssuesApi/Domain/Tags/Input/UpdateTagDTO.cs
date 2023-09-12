using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Domain.Inputs;

public class UpdateTagDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
