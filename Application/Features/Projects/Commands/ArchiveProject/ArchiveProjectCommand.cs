using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Commands.ArchiveProject
{
    public record ArchiveProjectCommand(int Id) : IRequest;
}
