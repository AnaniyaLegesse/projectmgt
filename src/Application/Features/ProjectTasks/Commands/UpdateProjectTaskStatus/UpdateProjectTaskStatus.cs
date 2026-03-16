using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProjectTasks.Commands.UpdateProjectTaskStatus;

public record UpdateProjectTaskStatusCommand(int Id, int UserId) : IRequest;

public class UpdateProjectTaskStatusHandler : IRequestHandler<UpdateProjectTaskStatusCommand>
{
    private readonly IProjectTaskRepository _repository;

    public UpdateProjectTaskStatusHandler(IProjectTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProjectTaskStatusCommand request, CancellationToken ct)
    {
        var task = await _repository.GetByIdAsync(request.Id, ct);

        if (task == null) throw new NotFoundException(nameof(ProjectTask), request.Id);

        // Call the Domain Method, don't set the property here!
        task.Complete(request.UserId);

        _repository.Update(task);
        await _repository.SaveChangesAsync(ct);
    }
}

