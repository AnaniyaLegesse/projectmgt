using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Projects.Queries.GetProjectById;
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectByIdHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken ct)
        {
            var project = await _repository.GetByIdAsync(request.Id, ct);

            if (project == null)
                throw new NotFoundException(nameof(Project), request.Id);

            return _mapper.Map<ProjectDto>(project);
        }
    }

