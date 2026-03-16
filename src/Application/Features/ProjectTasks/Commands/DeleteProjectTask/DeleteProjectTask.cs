using MediatR;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.DeleteProjectTask
{

    public record DeleteProjectTaskCommand(int Id) : IRequest;

    public class DeleteProjectTaskHandler : IRequestHandler<DeleteProjectTaskCommand>
    {
        private readonly IProjectTaskRepository _repository;

        public DeleteProjectTaskHandler(IProjectTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (task == null) throw new NotFoundException(nameof(ProjectTask), request.Id);



            _repository.Delete(task);
            await _repository.SaveChangesAsync(cancellationToken);
        }

    }

}

