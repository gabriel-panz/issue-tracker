using IssuesApi.Classes.Hateoas;

namespace IssuesApi.Classes;

public class ResponseViewModel<T>
{
    public string Message { get; set; } = "";
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<ApiAction>? Actions { get; set; }
}
