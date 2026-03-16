using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObj;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.UpdateProjectTaskPriority;


public record UpdateProjectTaskPriorityCommand(int Id, Priority NewPriority) : IRequest;

public class UpdateProjectTaskPriorityHandler : IRequestHandler<UpdateProjectTaskPriorityCommand>
{
    private readonly IProjectTaskRepository _repository;

    public UpdateProjectTaskPriorityHandler(IProjectTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProjectTaskPriorityCommand request, CancellationToken ct)
    {
        var task = await _repository.GetByIdAsync(request.Id, ct);

        if (task == null) throw new NotFoundException(nameof(ProjectTask), request.Id);

        // Use the domain method
        task.UpdatePriority(request.NewPriority);

        _repository.Update(task);
        await _repository.SaveChangesAsync(ct);
    }
}
