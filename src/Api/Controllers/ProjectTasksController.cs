using Application.Common.Models;
using Application.Features.ProjectTasks.Commands.CreateProjectTask;
using Application.Features.ProjectTasks.Commands.DeleteProjectTask;
using Application.Features.ProjectTasks.Commands.UpdateProjectTaskPriority;
using Application.Features.ProjectTasks.Commands.UpdateProjectTaskStatus;
using Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using Application.Features.ProjectTasks.Queries.GetProjectTaskList;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProjectTasksController : ApiControllerBase
{
    // GET: api/ProjectTasks/project/5
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<List<ProjectTaskDto>>> GetByProject(int projectId)
    {
        return await Mediator.Send(new GetProjectTaskListQuery(projectId));
    }

    // GET: api/ProjectTasks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskDto>> GetById(int id)
    {
        return await Mediator.Send(new GetProjectTaskByIdQuery(id));
    }

    // POST: api/ProjectTasks
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProjectTaskCommand command)
    {
        return await Mediator.Send(command);
    }

    // PATCH: api/ProjectTasks/5/status
    [HttpPatch("{id}/status")]
    public async Task<ActionResult> UpdateStatus(int id, UpdateProjectTaskStatusCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch");

        await Mediator.Send(command);
        return NoContent();
    }

    // PATCH: api/ProjectTasks/5/priority
    [HttpPatch("{id}/priority")]
    public async Task<ActionResult> UpdatePriority(int id, UpdateProjectTaskPriorityCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch");

        await Mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/ProjectTasks/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProjectTaskCommand(id));
        return NoContent();
    }
}