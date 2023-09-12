namespace IssuesApi.Domain.Outputs;

public class UserOutputDTO
{
    public long Id { get; set; }
    public string Login { get; set; } = null!;
    public string? Email { get; set; }
    public string? Nickname { get; set; }
}
