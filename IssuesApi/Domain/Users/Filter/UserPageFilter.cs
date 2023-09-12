using IssuesApi.Classes.Pagination;

namespace IssuesApi.Domain.Filters;

public class UsersPageFilter : PageFilter
{
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? Nickname { get; set; }
}
