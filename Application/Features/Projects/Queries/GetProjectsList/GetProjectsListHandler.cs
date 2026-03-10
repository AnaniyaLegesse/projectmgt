using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListHandler : IRequestHandler<GetProjectsListQuery, List<ProjectDto>>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectsListHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsListQuery request, CancellationToken ct)
        {
           var projects = await _repository.GetAllAsync(ct);

            return _mapper.Map<List<ProjectDto>>(projects);
        }
    }
}
