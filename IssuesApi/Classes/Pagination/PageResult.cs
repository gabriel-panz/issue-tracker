namespace IssuesApi.Classes.Pagination;

public class PageResult<T>
{
    /// <summary> Registros contidos na página. </summary>
    public List<T> Data { get; }
    private readonly PageFilter Filter;

    /// <summary> Índice da página. </summary>
    public int PageIndex { get => this.Filter.Index; }

    /// <summary> Quantidade de registros por página. </summary>
    public int PageSize { get => this.Filter.Size; }

    /// <summary> Quantidade total de registros em todas as páginas. </summary>
    public long TotalItems { get; }

    /// <summary> Quantidade total de páginas. </summary>
    public int TotalPages
    {
        get
        {
            if (TotalItems is 0) return 0;
            return Convert.ToInt32(Math.Ceiling(TotalItems / (double)PageSize));
        }
    }

    public PageResult(List<T> data, int index, byte size, long total)
    {
        Data = data;
        Filter = new PageFilter(index, size);

        TotalItems = total;
    }
    public PageResult(List<T> data, PageFilter filter, long total)
    {
        Data = data;
        Filter = filter;

        TotalItems = total;
    }
    public static readonly PageResult<T> Empty = new(new(), 1, 10, 0);

}
