using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Repositories.Interfaces;

public interface IIssuesRepository : IRepository<IssueItem>
{ }
