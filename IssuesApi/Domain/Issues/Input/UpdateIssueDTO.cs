using System.ComponentModel.DataAnnotations;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs;

namespace IssuesApi.Domain.Inputs;

public class UpdateIssueDTO
{
    public long Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public IssueStatus Status { get; set; }
    public string? Description { get; set; }

    [Required]
    public long ProjectId { get; set; }

    public List<long> TagIds { get; set; } = new();
}
