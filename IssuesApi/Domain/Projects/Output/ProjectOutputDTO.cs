using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.Outputs;

public class ProjectOutputDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}
