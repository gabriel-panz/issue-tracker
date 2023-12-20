namespace IssuesApi.Classes.Hateoas;

public class BodyField
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required bool Required { get; set; }
    public string? Description { get; set; }
}
