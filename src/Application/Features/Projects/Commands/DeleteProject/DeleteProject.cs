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
    public record DeleteProjectCommand(int Id) : IRequest;

    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository _projectRepository; 

        public DeleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(DeleteProjectCommand request, CancellationToken ct)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id, ct);

            if (project == null) 
                throw new NotFoundException(nameof(Project), request.Id);

            _projectRepository.Delete(project);
            await _projectRepository.SaveChangesAsync(ct);
        }
    }
}