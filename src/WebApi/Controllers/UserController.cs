using Application.Common.Models;
using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Commands.DeleteUser;
using Application.UseCases.User.Commands.UpdateUser;
using Application.UseCases.User.Queries.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetWithPagination([FromQuery] GetUserWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteUserCommand(id));

        return NoContent();
    }
}
