using Application.Common.Models;
using Application.UseCases.Post.Commands.CreatePost;
using Application.UseCases.Post.Commands.DeletePost;
using Application.UseCases.Post.Commands.UpdatePost;
using Application.UseCases.Post.Queries.GetPost;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PostController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PostEntity>>> GetWithPagination([FromQuery] GetPostWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async Task<ActionResult> Update(int id, UpdatePostCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePostCommand(id));

        return NoContent();
    }
}
