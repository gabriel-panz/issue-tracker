using System.Runtime.Serialization;

namespace IssuesApi.Classes.Exceptions;

public class ConflictException : Exception
{
    public ConflictException()
    {
    }

    public ConflictException(string? message) : base(message)
    {
    }

    protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
    public static ConflictException Create(string value)
        => new($"unique constraint failure:{value.Remove(value.Length - 2, 2)}");
}
