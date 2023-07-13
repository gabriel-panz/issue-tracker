using LanguageExt;

namespace IssuesApi.Classes;

public class ResponseViewModel<T>
{
    public string Message { get; set; } = "";
    public bool Success { get; set; }
    public Option<T> Data { get; set; }
}
