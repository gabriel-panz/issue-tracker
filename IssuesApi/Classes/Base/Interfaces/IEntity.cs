namespace IssuesApi.Classes.Base.Interfaces;

public interface IEntity
{
    public long Id { get; set; }
    public bool IsEnabled { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
