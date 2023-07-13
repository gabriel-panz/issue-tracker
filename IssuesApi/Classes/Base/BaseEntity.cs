using IssuesApi.Classes.Base.Interfaces;

namespace IssuesApi.Classes.Base;

public class BaseEntity : IEntity
{
    public long Id { get; set; }
    public bool IsEnabled { get; set; }

    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
