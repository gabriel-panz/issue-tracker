namespace IssuesApi.Classes.Hateoas;

public class ApiAction
{
    public required string Method { get; set; }
    public required string Href { get; set; }
    public required string Rel { get; set; }
    public List<BodyField>? Fields { get; set; }
}
