using IssuesApi.Classes.Base;

namespace IssuesApi.Domain.Entities;

public class IssueItem : BaseEntity
{
    public string Title { get; set; } = "";
    public IssueStatus Status { get; set; }
    public string? Description { get; set; }

    public Project Project { get; set; }
    public long ProjectId { get; set; }
}

public enum IssueStatus
{
    OPEN = 1,
    IN_PROGRESS = 2,
    CLOSED = 3
}