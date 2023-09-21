using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuesApi.Classes.Base;
using IssuesApi.Domain.Input;
using IssuesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;


public class AuthController : BaseController
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
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