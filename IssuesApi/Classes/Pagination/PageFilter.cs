namespace IssuesApi.Classes.Pagination;

public class PageFilter
{
    public int Index { get; set; }
    public byte Size { get; set; }
    public int Skip => (this.Index - 1) * Size;

    public PageFilter(int index, byte size)
    {
        // Nunca menos que 1
        this.Index = index < 1
            ? 1
            : index;
    }
}