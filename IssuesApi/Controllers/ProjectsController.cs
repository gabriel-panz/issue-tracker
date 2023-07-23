using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;

public class ProjectsController : BaseController<ProjectDTO, Project>
{
    public ProjectsController(
        IProjectsService service)
        : base(service)
    { }

    [HttpGet(Name = "[action][controller]")]
    public async Task<IActionResult> GetPage(
    [Required][DefaultValue(1)] int index,
    [Required][DefaultValue(10)] byte size)
    {
        var result = await _service.GetPage(new PageFilter(index, size));
        return result.Match<IActionResult>(
            Succ: s => OkResult<PageResult<ProjectDTO>>(s),
            Fail: e => BadResult(e)
        );
    }
}
