using IssuesApi.Classes.Base;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssuesApi.Controllers;

public class IssuesController : BaseController<IssueItemDTO, IssueItem>
{
    public IssuesController(IIssuesService service) : base(service)
    {
    }

    [HttpGet("statuses")]
    public IActionResult GetIssueStatusList()
    {
        var list = Enum.GetValues<IssueStatus>()
            .ToList();
        
        return OkResult<List<IssueStatus>>(list);
    }
}
