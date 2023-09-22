namespace IssuesApi.Classes.Exceptions;

public class UnauthorizedAccessToResourceException : Exception
{
    public UnauthorizedAccessToResourceException()
    {
    }

    public UnauthorizedAccessToResourceException(string? message) : base(message)
    {
    }

    public static UnauthorizedAccessToResourceException Create(long entityId, long userId)
    {
        var message = $"User Id {userId} does not have access to resource with id {entityId}";
        return new UnauthorizedAccessToResourceException(message);
    }
}
