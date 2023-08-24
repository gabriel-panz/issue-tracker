namespace IssuesApi.Classes.Pagination;

public class FilteredList<T>
{
    public FilteredList(List<T> data, long total)
    {
        TotalCount = total;
        Data = data;
    }
    public List<T> Data { get; set; } = new();
    public long TotalCount { get; set; }
}
