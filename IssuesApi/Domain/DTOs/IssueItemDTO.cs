using System.Text.Json.Serialization;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.DTOs;

public class IssueItemDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public IssueStatus Status { get; set; } = IssueStatus.OPEN;
    public string? Description { get; set; }
}
