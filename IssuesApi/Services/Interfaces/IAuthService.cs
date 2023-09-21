using IssuesApi.Domain.Input;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IAuthService
{
    Task<Result<string>> LogIn(LogInDTO logIn);
}