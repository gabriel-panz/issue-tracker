using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Classes.Base;

[ApiController]
[Route("v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    public BaseController()
    { }

    protected virtual void AddActions<D>(ResponseViewModel<D> model)
    { }

    protected IActionResult OkResult<D>(D data, string message = "Success")
    {
        var res = new ResponseViewModel<D>
        {
            Message = message,
            Success = true,
            Data = data
        };

        AddActions(res);

        return Ok(res);
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
