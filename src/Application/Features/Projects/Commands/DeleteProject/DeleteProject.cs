using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Projects.Commands.DeleteProject
{
   

    namespace Application.Features.ProjectTasks.Commands.DeleteProject
    {

        public record DeleteProjectCommand(int Id) : IRequest;

        public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand>
        {
            private readonly IProjectTaskRepository _repository;

            public DeleteProjectHandler(IProjectTaskRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var task = await _repository.GetByIdAsync(request.Id, cancellationToken);

                if (task == null) throw new NotFoundException(nameof(ProjectTask), request.Id);



                _repository.Delete(task);
                await _repository.SaveChangesAsync(cancellationToken);
            }
        }

    }


}
