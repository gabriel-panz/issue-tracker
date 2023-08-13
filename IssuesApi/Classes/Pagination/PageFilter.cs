using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IssuesApi.Classes.Pagination;

public class PageFilter
{
    [Required]
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int Index { get; set; }

    [Required]
    [DefaultValue(10)]
    [Range(1, byte.MaxValue)]
    public byte Size { get; set; }

    public int Skip() => (Index - 1) * Size;

    public PageFilter()
    { }

    public PageFilter(int index, byte size)
    {
        // Nunca menos que 1
        Index = index < 1
            ? 1
            : index;

        Size = size < 1
            ? (byte)1
            : size;
    }
}