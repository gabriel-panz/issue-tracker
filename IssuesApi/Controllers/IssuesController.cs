using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;

public class IssuesController : BaseController<IssueItemDTO, IssueItem>
{
    private new readonly IIssuesService _service;
    public IssuesController(IIssuesService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("statuses")]
    public IActionResult GetIssueStatusList()
    {
        var list = Enum.GetValues<IssueStatus>()
            .ToList();

        return OkResult<List<IssueStatus>>(list);
    }

    [HttpGet(Name = "[action][controller]")]
    public async Task<IActionResult> GetPage(
        [Required] long projectId,
        [Required][DefaultValue(1)] int index,
        [Required][DefaultValue(10)] byte size)
    {
        var result = await _service.GetPage(projectId, new PageFilter(index, size));
        return result.Match<IActionResult>(
            Succ: s => OkResult<PageResult<IssueItemDTO>>(s),
            Fail: e => BadResult(e)
        );
    }
}
