using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Queries.GetProjectById
{
    public record GetProjectByIdQuery(int Id) : IRequest<ProjectDto>;
}
