using Application.Common.Models;
using Application.UseCases.Tag.Commands.CreateTag;
using Application.UseCases.Tag.Commands.DeleteTag;
using Application.UseCases.Tag.Commands.UpdateTag;
using Application.UseCases.Tag.Queries.GetTag;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class TagController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<TagEntity>>> GetWithPagination([FromQuery] GetTagWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdateTagCommand command)
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
        await Mediator.Send(new DeleteTagCommand(id));

        return NoContent();
    }
}
