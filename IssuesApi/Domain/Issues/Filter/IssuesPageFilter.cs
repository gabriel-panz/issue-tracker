using System.ComponentModel.DataAnnotations;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.Filters;

public class IssuesPageFilter : PageFilter
{
    [Required]
    public long ProjectId { get; set; }
    public List<long> TagIds { get; set; } = new();
    public List<IssueStatus> Status { get; set; } = new();
}
