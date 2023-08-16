using Application.UseCases.Category.Commands.CreateCategory;
using Application.UseCases.Category.Commands.DeleteCategory;
using Bogus;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IntegrationTest.Category.Commands;

[TestClass]
public class DeleteCategoryTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldDeleteCategoryUseCase()
    {
        var createCategoryCommand = CreateCategoryTest.GenerateCreateCategoryCommand();
        var createdCategoryId = await SendAsync(createCategoryCommand);
        Assert.IsNotNull(createdCategoryId);
        Assert.IsTrue(createdCategoryId > 0);

        await SendAsync(new DeleteCategoryCommand(createdCategoryId));

        var category = await FindAsync<CategoryEntity>(createdCategoryId);
        Assert.IsNull(category);
    }

    [TestMethod]
    public async Task ShouldDeleteCategoryWebApi()
    {
        var createCategoryCommand = CreateCategoryTest.GenerateCreateCategoryCommand();
        var createdCategoryId = await SendAsync(createCategoryCommand);
        Assert.IsNotNull(createdCategoryId);
        Assert.IsTrue(createdCategoryId > 0);

        using var client = CreateHttpClient();
        var response = await client.DeleteAsync($"/api/category?id={createdCategoryId}");

        var category = await FindAsync<CategoryEntity>(createdCategoryId);
        Assert.IsNull(category);
    }
}
