using IssuesApi.Classes.Pagination;

namespace IssuesApi.Domain.Filter;

public class TagFilter
{ 
    public long? Id { get; set; }
    public string? Name { get; set; }
}
