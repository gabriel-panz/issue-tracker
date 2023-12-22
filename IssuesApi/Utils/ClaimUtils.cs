namespace IssuesApi.Utils;

public static class ClaimUtils
{
    public static long GetLoggedUserId(this HttpContext context)
    {
        return long.Parse(context.User.Claims
            .Where(x => x.Type == ClaimTypes.UserId.ToString())
            .First().Value);
    }
}
