using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.DTOs;

public class IssueItemDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public IssueStatus Status { get; set; }
    public string? Description { get; set; }
}
