using Application.Common.Models;
using Application.UseCases.Post.Commands.CreatePost;
using Application.UseCases.Post.Commands.DeletePost;
using Application.UseCases.Post.Commands.UpdatePost;
using Application.UseCases.Post.Queries.GetPost;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PostController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<PostEntity>>> GetWithPagination([FromQuery] GetPostWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, UpdatePostCommand command)
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
        await Mediator.Send(new DeletePostCommand(id));

        return NoContent();
    }
}
