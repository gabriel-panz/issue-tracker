namespace IssuesApi.Domain.Inputs;

public class UpdateProjectDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
