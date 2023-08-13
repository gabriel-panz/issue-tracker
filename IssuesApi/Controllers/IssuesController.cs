using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Issues;
using IssuesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;

public class IssuesController : BaseController
{
    private readonly IIssuesService _service;
    public IssuesController(IIssuesService service) : base()
    {
        _service = service;
    }

    [HttpGet("statuses")]
    public IActionResult GetIssueStatusList()
    {
        var list = Enum.GetValues<IssueStatus>()
            .ToList();

        return OkResult(list);
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
    public async Task<IActionResult> Create([FromBody] CreateIssueDTO dto)
    {
        var result = await _service.Create(dto);

        return result.Match(
            Succ: s => OkResult(s),
            Fail: BadResult
        );
    }

    [HttpPut("{id}", Name = "[action][controller]")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] CreateIssueDTO dto)
    {
        var result = await _service.Update(id, dto);

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
        [Required] long projectId,
        [Required][DefaultValue(1)] int index,
        [Required][DefaultValue(10)] byte size)
    {
        var result = await _service
            .GetPage(projectId, new PageFilter(index, size));

        return result.Match(
            Succ: s => OkResult(s),
            Fail: BadResult
        );
    }
}
