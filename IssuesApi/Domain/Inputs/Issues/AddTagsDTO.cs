namespace IssuesApi.Domain.Inputs.Issues;

public class UpdateTagsDTO
{
    public List<long> TagIds { get; set; }
    public long IssueId { get; set; }
}
