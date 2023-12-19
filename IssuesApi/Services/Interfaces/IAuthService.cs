using IssuesApi.Classes;
using IssuesApi.Domain.Input;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> LogIn(LogInDTO logIn);
}