using IssuesApi.Classes.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

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

    public static Exception ParseDbUpdateException(this DbUpdateException e)
    {
        if (e.InnerException is null) return e;

        if (e.InnerException is SqliteException se)
        {
            ReadOnlySpan<string> values = se.Message.Split(":");
            if (values[0] is "SQLite Error 19")
            {
                if (values[1].Contains("UNIQUE"))
                    return ConflictException.Create(values[2]);
            }
        }

        return e;
    }
}
