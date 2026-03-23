using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObj;
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

        public async Task<int> Handle(CreateProjectTaskCommand request, CancellationToken ct)
        {
            // Use your existing Parse method to convert the string
            // This handles casing (e.g., "High" or "high") automatically
            var priority = Priority.Parse(request.PriorityLevel);

            var projectTask = new ProjectTask(
                request.Title,
                priority,
                request.ProjectId
            );

            _repository.Add(projectTask);
            await _repository.SaveChangesAsync(ct);

            return projectTask.Id;
        }
    }
}
