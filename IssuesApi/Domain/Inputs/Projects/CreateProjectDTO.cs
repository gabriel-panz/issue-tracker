namespace IssuesApi.Domain.Inputs.Projects;

public class CreateProjectDTO
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
