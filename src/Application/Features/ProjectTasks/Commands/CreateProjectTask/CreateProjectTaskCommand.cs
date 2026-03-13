using Domain.ValueObj;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommand : IRequest<int>
    {
        public string Title { get; set; }
        public Priority Priority { get; set; }

        public int ProjectId { get; set; }
    }
}
