namespace IssuesApi.Classes.Hateoas;
public interface IHateoasResponse
{
    public List<ApiAction> Actions { get; set; }
}
