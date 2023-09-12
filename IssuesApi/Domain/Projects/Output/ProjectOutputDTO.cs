namespace IssuesApi.Domain.Outputs;

public class ProjectOutputDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public int OpenIssues { get; set; }
    public int ActiveIssues { get; set; }
    public int ClosedIssues { get; set; }
}
