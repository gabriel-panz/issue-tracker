using System.ComponentModel.DataAnnotations;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.Filters.Issues;

public class IssuesPageFilter : PageFilter
{
    [Required]
    public long ProjectId { get; set; }
    public List<IssueStatus> Status { get; set; } = new();
}