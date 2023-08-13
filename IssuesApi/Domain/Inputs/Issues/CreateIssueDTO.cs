using System.ComponentModel.DataAnnotations;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.Inputs.Issues;

public class CreateIssueDTO
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public IssueStatus Status { get; set; }
    public string? Description { get; set; }

    [Required]
    public long ProjectId { get; set; }

    public List<long> TagIds { get; set; } = new();
}
