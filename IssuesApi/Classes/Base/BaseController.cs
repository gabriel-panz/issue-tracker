using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Classes.Base;

[ApiController]
[Route("v1/[controller]")]
public abstract class BaseController<DTO, T> : ControllerBase
    where T : class, IEntity
{
    protected readonly IService<DTO, T> _service;
    public BaseController(IService<DTO, T> service)
    {
        _service = service;
    }

    [HttpGet("{id}", Name = "[action][controller]")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var result = await _service.Get(id);
        return result.Match<IActionResult>(
            Succ: s => OkResult(s),
            Fail: e => BadResult(e)
        );
    }

    [HttpPost(Name = "[action][controller]")]
    public async Task<IActionResult> Create([FromBody] DTO dto)
    {
        var result = await _service.Create(dto);
        return result.Match<IActionResult>(
            Succ: s => OkResult<DTO>(s),
            Fail: e => BadResult(e)
        );
    }

    [HttpPut("{id}", Name = "[action][controller]")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] DTO dto)
    {
        var result = await _service.Update(id, dto);
        return result.Match<IActionResult>(
            Succ: s => OkResult(s),
            Fail: e => BadResult(e)
        );
    }

    [HttpDelete("{id}", Name = "[action][controller]")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var result = await _service.SoftDelete(id);
        return result.Match<IActionResult>(
            Succ: s => OkResult(true),
            Fail: e => BadResult(e)
        );
    }

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
