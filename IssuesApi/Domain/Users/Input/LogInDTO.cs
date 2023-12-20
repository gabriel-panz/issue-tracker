using IssuesApi.Classes.Hateoas;

namespace IssuesApi.Domain.Input;

public class LogInDTO
{
    public LogInDTO()
    {
        Login = " ";
        Password = " ";
    }

    public required string Login { get; set; }
    public required string Password { get; set; }

    public static List<BodyField> GetFields()
    {
        return new()
        {
            new()
            {
                Name = "login",
                Type = "string",
                Required = true
            },
            new()
            {
                Name = "password",
                Type = "string",
                Required = true
            }
        };
    }
}
