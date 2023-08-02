using Application.Common.Models;
using Application.UseCases.Tag.Commands.CreateTag;
using Application.UseCases.Tag.Commands.DeleteTag;
using Application.UseCases.Tag.Commands.UpdateTag;
using Application.UseCases.Tag.Queries.GetTag;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class TagController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<TagEntity>>> GetWithPagination([FromQuery] GetTagWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async Task<ActionResult> Update(int id, UpdateTagCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTagCommand(id));

        return NoContent();
    }
}
