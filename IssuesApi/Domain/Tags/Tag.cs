using IssuesApi.Classes.Base;

namespace IssuesApi.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public long CreatedByUserId { get; set; }

    public User? CreatedByUser { get; set; }
    public ICollection<IssueTag> IssueTags { get; set; } = null!;
}
