using Api.Controllers;
using Application.Common.Models;
using Application.Features.Projects.Commands;
using Application.Features.Projects.Commands.ArchiveProject;
using Application.Features.Projects.Commands.CreateProject;
using Application.Features.Projects.Queries;
using Application.Features.Projects.Queries.GetProjectsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProjectsController : ApiControllerBase
{
    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> Get()
    {
        return await Mediator.Send(new GetProjectsListQuery());
    }

    // POST: api/projects
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProjectCommand command)
    {
        return await Mediator.Send(command);
    }

    // PUT: api/projects/5/archive
    [HttpPut("{id}/archive")]
    public async Task<ActionResult> Archive(int id)
    {
        await Mediator.Send(new ArchiveProjectCommand { Id = id });
        return NoContent();
    }
}