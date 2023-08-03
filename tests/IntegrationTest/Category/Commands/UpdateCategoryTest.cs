using Application.UseCases.Category.Commands.UpdateCategory;
using Domain.Entities;

namespace IntegrationTest.Category.Commands;

[TestClass]
public class UpdateCategoryTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateCategory()
    {
        var createCategoryCommand = CreateCategoryTest.GenerateCreateCategoryCommand();
        var createdCategoryId = await SendAsync(createCategoryCommand);
        Assert.IsNotNull(createdCategoryId);
        Assert.IsTrue(createdCategoryId > 0);

        var category = await FindAsync<CategoryEntity>(createdCategoryId);

        Assert.IsNotNull(category);
        Assert.IsTrue(category.Id > 0);
        Assert.IsTrue(category.Name == createCategoryCommand.Name);

        category.Name = $"updated_{category.Name}";

        await SendAsync(new UpdateCategoryCommand
        {
            Id = category.Id,
            Name = category.Name,
        });

        category = await FindAsync<CategoryEntity>(createdCategoryId);

        Assert.IsNotNull(category);
        Assert.IsTrue(category.Name == $"updated_{createCategoryCommand.Name}");
    }
}
