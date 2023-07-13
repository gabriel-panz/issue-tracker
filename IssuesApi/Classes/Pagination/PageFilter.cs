using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Classes.Pagination;

public class PageFilter
{
    [Required]
    public int Index { get; set; }
    [Required]
    public byte Size { get; set; }
    public readonly int Skip;

    public PageFilter(int index, byte size)
    {
        // Nunca menos que 1
        this.Index = index < 1
            ? 1
            : index;
        this.Skip = (this.Index - 1) * Size;
    }
}