using System.Runtime.Serialization;

namespace IssuesApi.Classes.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static UserNotFoundException Create()
    {
        var message = $"User Login or Password is incorrect";
        return new UserNotFoundException(message);

    }
}
