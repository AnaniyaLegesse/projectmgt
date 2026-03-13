using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Queries.GetProjectTaskList
{
    public record GetProjectTaskListQuery(int Id) : IRequest<List<ProjectTaskDto>>;
}
