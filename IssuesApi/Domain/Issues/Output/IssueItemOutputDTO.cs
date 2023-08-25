using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.Outputs;

public class IssueItemOutputDTO
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
    public string Title { get; set; } = null!;
    public IssueStatus Status { get; set; }
    public string? Description { get; set; }

    public List<TagOutputDTO> Tags { get; set; } = new();
}