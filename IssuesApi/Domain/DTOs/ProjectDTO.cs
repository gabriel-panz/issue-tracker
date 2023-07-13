namespace IssuesApi.Domain.DTOs;

public class ProjectDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}
