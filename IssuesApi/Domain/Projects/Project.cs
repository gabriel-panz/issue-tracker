using IssuesApi.Classes.Base;

namespace IssuesApi.Domain.Entities;

public class Project : BaseEntity
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public long CreatedByUserId { get; set; }
    public List<IssueItem> Issues { get; set; } = new();
    public User? CreatedByUser { get; set; }
}
