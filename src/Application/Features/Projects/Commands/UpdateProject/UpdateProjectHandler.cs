using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Projects.Commands.CreateProject;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IProjectRepository _repository;

        public UpdateProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProjectCommand request, CancellationToken ct)
        {
            // The repository handles the logic of finding the project
            var entity = await _repository.GetByIdAsync(request.Id, ct);

            if (entity == null)
                throw new NotFoundException(nameof(Project), request.Id);

            // Call the Domain logic method
            entity.UpdateDetails(request.Name, request.Description, request.Deadline);

            // Persist changes
            await _repository.SaveChangesAsync(ct);
        }
    }
}
