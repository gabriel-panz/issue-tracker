using IssuesApi.Classes.Base.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Classes.Base;

[ApiController]
[Route("v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    public BaseController()
    { }

    protected IActionResult OkResult<D>(D data, string message = "Success")
    {
        return Ok(new ResponseViewModel<D>
        {
            Message = message,
            Success = true,
            Data = data
        });
    }

    protected IActionResult BadResult(Exception data)
    {
        return BadRequest(new ResponseViewModel<Exception>
        {
            Message = data.Message,
            Success = false,
            Data = null
        });
    }
}
