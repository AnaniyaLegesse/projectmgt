using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tasks.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdHandler : IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskDto>
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectTaskByIdHandler(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProjectTaskDto> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var projectTask = await _repository.GetByIdAsync(request.id, cancellationToken);

            if (projectTask == null)
            {
                throw new ArgumentNullException(nameof(projectTask));
            }

            return _mapper.Map<ProjectTaskDto>(projectTask);
        }
    }
}
