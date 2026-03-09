using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // 1. Create the Domain Entity using the constructor (which enforces rules)
            var entity = new Project(request.Name, request.Description, request.Deadline);

            // 2. Add to the Change Tracker
            _context.Projects.Add(entity);

            // 3. Persist to Database
            await _context.SaveChangesAsync(cancellationToken);

            // 4. Return the new ID
            return entity.Id;
        }
    }
}
