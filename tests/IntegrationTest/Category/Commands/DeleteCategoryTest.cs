using Application.UseCases.Category.Commands.CreateCategory;
using Application.UseCases.Category.Commands.DeleteCategory;
using Bogus;
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
    public async Task ShouldDeleteCategory()
    {
        var createCategoryCommand = CreateCategoryTest.GenerateCreateCategoryCommand();
        var createdCategoryId = await SendAsync(createCategoryCommand);
        Assert.IsNotNull(createdCategoryId);
        Assert.IsTrue(createdCategoryId > 0);

        await SendAsync(new DeleteCategoryCommand(createdCategoryId));

        var category = await FindAsync<CategoryEntity>(createdCategoryId);
        Assert.IsNull(category);
    }
}
