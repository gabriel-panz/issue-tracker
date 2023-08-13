using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.Inputs.Issues;

public class CreateIssueDTO
{
    public string Title { get; set; } = null!;
    public IssueStatus Status { get; set; }
    public string? Description { get; set; }

    public long ProjectId { get; set; }
}
