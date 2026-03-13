using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand, int>
    {
       private IProjectTaskRepository _repository;

        public CreateProjectTaskCommandHandler(IProjectTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {

            var projectTask = new ProjectTask(
            request.Title,
            request.Priority,
            request.ProjectId
             );

            
            _repository.Add(projectTask);

          
            await _repository.SaveChangesAsync(cancellationToken);

            
            return projectTask.Id;
        }
    }
}
