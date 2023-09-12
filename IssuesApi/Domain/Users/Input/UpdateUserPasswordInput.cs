namespace IssuesApi.Domain.Inputs;

public class UpdateUserPasswordInput
{
    public long Id { get; set; }
    public required string Password { get; set; }
}
