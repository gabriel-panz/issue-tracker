using System.Runtime.CompilerServices;
using IssuesApi.Classes.Hateoas;

namespace IssuesApi.Utils;
public static class DtoUtils
{
    public static List<BodyField> GetFields<T>()
    {
        var r = new List<BodyField>();
        var type = typeof(T);
        var props = type.GetProperties();
        foreach (var p in props)
        {
            r.Add(new()
            {
                Name = p.Name,
                Required = Attribute.IsDefined(p, typeof(RequiredMemberAttribute)),
                Type = p.PropertyType.Name
            });
        }
        return r;
    }
}
