using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Commands.ArchiveProject
{
    public class ArchiveProjectCommandHandler : IRequestHandler<ArchiveProjectCommand>
    {
        private readonly IProjectRepository _repository;

        public ArchiveProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ArchiveProjectCommand request, CancellationToken ct)
        {
            // The Repository is responsible for including the Tasks
            // so the business rule inside entity.Archive() can run.
            var entity = await _repository.GetByIdWithTasksAsync(request.Id, ct);

            if (entity == null)
                throw new NotFoundException(nameof(Project), request.Id);

            // This method inside the Domain Entity checks the business rules
            entity.Archive();

            await _repository.SaveChangesAsync(ct);
        }
    }
}
