using IssuesApi.Classes.Base;

namespace IssuesApi.Domain.Entities;

public class User : BaseEntity
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public string? Email { get; set; }
    public string? Nickname { get; set; }

    public List<Project> Projects { get; set; } = new();
}
