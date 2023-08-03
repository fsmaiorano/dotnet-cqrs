using Application.Common.Models;
using Application.UseCases.Category.Commands.CreateCategory;
using Application.UseCases.Category.Commands.DeleteCategory;
using Application.UseCases.Category.Commands.UpdateCategory;
using Application.UseCases.Category.Queries.GetCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CategoryController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<CategoryEntity>>> GetWithPagination([FromQuery] GetCategoryWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async Task<ActionResult> Update(int id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}
