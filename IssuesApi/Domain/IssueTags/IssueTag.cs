namespace IssuesApi.Domain.Entities;

public class IssueTag
{
    public long Id { get; set; }

    public long TagId { get; set; }
    public Tag Tag { get; set; } = null!;

    public long IssueId { get; set; }
    public IssueItem IssueItem { get; set; } = null!;
}
