namespace IssuesApi.Domain.Inputs;

public class UpdateUserDTO
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public string? Email { get; set; }
    public string? Nickname { get; set; }
}
