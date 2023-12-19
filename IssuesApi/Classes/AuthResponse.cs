namespace IssuesApi.Classes;

public class AuthResponse
{
    public required string Token { get; set; }
    public required long UserId { get; set; }
}
