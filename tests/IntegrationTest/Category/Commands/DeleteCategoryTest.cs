using Application.UseCases.Category.Commands.DeleteCategory;
using Domain.Entities;

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

        using var client = await CreateHttpClient();
        var response = await client.DeleteAsync($"/api/category?id={createdCategoryId}");

        var category = await FindAsync<CategoryEntity>(createdCategoryId);
        Assert.IsNull(category);
    }
}
