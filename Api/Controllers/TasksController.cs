//using Api.Controllers;
//using Application.Features.Tasks.Commands;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace Api.Controllers;

//public class TasksController : ApiControllerBase
//{
//    // POST: api/tasks
//    [HttpPost]
//    public async Task<ActionResult<int>> Create(CreateTaskCommand command)
//    {
//        return await Mediator.Send(command);
//    }

//    // PATCH: api/tasks/5/status
//    [HttpPatch("{id}/status")]
//    public async Task<ActionResult> UpdateStatus(int id, UpdateTaskStatusCommand command)
//    {
//        // Ensure the ID in the route matches the ID in the body
//        if (id != command.Id) return BadRequest("ID mismatch");

//        await Mediator.Send(command);
//        return NoContent();
//    }
//}