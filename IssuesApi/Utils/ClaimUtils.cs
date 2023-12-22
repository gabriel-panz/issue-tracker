namespace IssuesApi.Utils;

public static class ClaimUtils
{
    public static long GetClaimsUserId(this HttpContext context)
    {
        var userId = context.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.UserId.ToString())?
            .Value;

        return userId is null
            ? throw new Exception("could not find userId in request context")
            : long.Parse(userId);
    }
}
