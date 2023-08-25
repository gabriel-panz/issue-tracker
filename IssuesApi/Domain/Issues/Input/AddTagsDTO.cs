namespace IssuesApi.Domain.Inputs;

public class UpdateTagsDTO
{
    public List<long> TagIds { get; set; } = new();
    public long IssueId { get; set; }
}
