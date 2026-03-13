using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand>
    {
        public Task Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
