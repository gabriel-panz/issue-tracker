using IssuesApi.Classes;
using IssuesApi.Classes.Base;
using IssuesApi.Domain.Input;
using IssuesApi.Domain.Inputs;
using IssuesApi.Services.Interfaces;
using IssuesApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }
    protected override void AddActions<D>(ResponseViewModel<D> model)
    {
        model.Actions = new()
        {
            new()
            {
                Href = "http://localhost:5001/v1/Projects",
                Method = "GET",
                Rel = "list-projects"
            },
            new()
            {
                Href = "http://localhost:5001/v1/Projects",
                Method = "POST",
                Rel = "create-project",
                Fields = DtoUtils.GetFields<CreateProjectDTO>()
            },
            new()
            {
                Href = "http://localhost:5001/v1/Tags",
                Method = "GET",
                Rel = "list-tags"
            },
            new()
            {
                Href = "http://localhost:5001/v1/Tags",
                Method = "POST",
                Rel = "create-tag",
                Fields = DtoUtils.GetFields<CreateTagDTO>()
            }
        };

        if (model.Data is AuthResponse r)
        {
            model.Actions.Add(new()
            {
                Href = $"http://localhost:5001/v1/Users/{r.UserId}",
                Method = "GET",
                Rel = "get-user"
            });

            model.Actions.Add(new()
            {
                Href = $"http://localhost:5001/v1/Users/",
                Method = "PUT",
                Rel = "edit-user",
                Fields = DtoUtils.GetFields<UpdateUserDTO>()
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInDTO logIn)
    {
        var result = await _service.LogIn(logIn);
        return result.Match(
            Succ: token => OkResult(token),
            Fail: BadResult
        );
    }
}