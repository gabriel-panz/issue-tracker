using IssuesApi.Classes.Base;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Inputs;
using IssuesApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;

[Authorize]
public class TagsController : BaseController
{
    private readonly ITagsService _service;
    public TagsController(ITagsService service) : base()
    {
        _service = service;
    }

    [HttpGet("{id}", Name = "[action][controller]")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var result = await _service.Get(id);
        return result.Match(
            Succ: s => OkResult(s),
            Fail: BadResult
        );
    }

    [HttpPost(Name = "[action][controller]")]
    public async Task<IActionResult> Create([FromBody] CreateTagDTO dto)
    {
        var result = await _service.Create(dto);

        return result.Match(
            Succ: s => OkResult(s),
            Fail: BadResult
        );
    }

    [HttpPut(Name = "[action][controller]")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateTagDTO dto)
    {
        var result = await _service.Update(dto);

        return result.Match(
            Succ: s => OkResult(s),
            Fail: BadResult
        );
    }

    [HttpDelete("{id}", Name = "[action][controller]")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var result = await _service.SoftDelete(id);

        return result.Match(
            Succ: s => OkResult(true),
            Fail: BadResult
        );
    }

    [HttpGet(Name = "[action][controller]")]
    public async Task<IActionResult> GetPage(
        [FromQuery] PageFilter filter)
    {
        var result = await _service
            .GetPage(filter);

        return result.Match(
            Succ: s => OkResult(s),
            Fail: BadResult
        );
    }
}
