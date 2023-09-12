using System.Runtime.Serialization;

namespace IssuesApi.Classes.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException()
    {
    }

    public ResourceNotFoundException(string? message) : base(message)
    {
    }

    protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static ResourceNotFoundException Create(string entity, string query)
    {
        var message = $"Entity {entity} with query {query} not found";
        return new ResourceNotFoundException(message);

    }
}
