using IssuesApi.Classes.Base;

namespace IssuesApi.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<IssueTag> IssueTags { get; set; } = null!;
}
