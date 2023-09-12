namespace IssuesApi.Domain.Inputs;

public class CreateUserDTO
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public string? Email { get; set; }
    public string? Nickname { get; set; }
}
