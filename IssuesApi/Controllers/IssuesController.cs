using IssuesApi.Classes.Base;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Services.Interfaces;

namespace IssuesApi.Controllers;

public class IssuesController : BaseController<IssueItemDTO, IssueItem>
{
    public IssuesController(IIssuesService service) : base(service)
    {
    }
}
