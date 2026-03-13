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

namespace Application.Features.Tasks.Queries.GetProjectTaskList
{
    public class GetProjectTaskListHandler : IRequestHandler<GetProjectTaskListQuery, List<ProjectTaskDto>>
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectTaskListHandler(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<List<ProjectTaskDto>> Handle(GetProjectTaskListQuery request, CancellationToken cancellationToken)
        {
            var projectTasks = await _repository.GetByProjectIdAsync(request.Id, cancellationToken);

            if (projectTasks == null)
                throw new NotFoundException(nameof(Project), request.Id);

            
            return _mapper.Map<List<ProjectTaskDto>>(projectTasks);
        }
    }
}
