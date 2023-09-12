using IssuesApi.Classes.Pagination;

namespace IssuesApi.Domain.Filter;

public class ProjectFilter : PageFilter
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}
