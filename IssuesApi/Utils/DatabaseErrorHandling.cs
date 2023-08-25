namespace IssuesApi.Utils;

public static class DatabaseErrorHandling
{
    public static string ConvertSqlite(string message)
    {
        ReadOnlySpan<string> values = message.Split("'");

        if (!values[0].Contains("Duplicate entry"))
            return message;

        var value = values[1];
        var constraint = values[3];

        var property = constraint.Split("_").Last();

        return property switch
        {
            "CardNumber" => $"442-{property}-{value}",
            "Plate" => $"443-{property}-{value}",
            _ => ""
        };
    }
}
