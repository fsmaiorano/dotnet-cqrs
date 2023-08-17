using System.Text;
using Application.UseCases.Category.Commands.UpdateCategory;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Category.Commands;

[TestClass]
public class UpdateCategoryTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateCategoryUseCase()
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

    [TestMethod]
    public async Task ShouldUpdateCategoryWebApi()
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

        var updateCategoryCommand = new UpdateCategoryCommand
        {
            Id = category.Id,
            Name = category.Name,
        };

        using var client = await CreateHttpClient();
        var response = await client.PutAsync($"/api/category?id={category.Id}", new StringContent(JsonConvert.SerializeObject(updateCategoryCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);

        category = await FindAsync<CategoryEntity>(createdCategoryId);

        Assert.IsNotNull(category);
        Assert.IsTrue(category.Name == $"updated_{createCategoryCommand.Name}");
    }
}
