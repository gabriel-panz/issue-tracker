using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssuesApi.Domain.Filter;

public class IssueTagFilter
{
    public List<long>? TagIds { get; set; }
    public List<long>? IssueIds { get; set; }
}