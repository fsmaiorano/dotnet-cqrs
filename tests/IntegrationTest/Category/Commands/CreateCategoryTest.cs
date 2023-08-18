using System.Text;
using Application.UseCases.Category.Commands.CreateCategory;
using Bogus;
using Newtonsoft.Json;

namespace IntegrationTest.Category.Commands;

[TestClass]
public class CreateCategoryTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldCreateCategoryUseCase()
    {
        var createCategoryCommand = GenerateCreateCategoryCommand();

        var createdCategoryId = await SendAsync(createCategoryCommand);
        Assert.IsNotNull(createdCategoryId);
        Assert.IsTrue(createdCategoryId > 0);
    }

    [TestMethod]
    public async Task ShouldCreateCategoryController()
    {
        var createCategoryCommand = GenerateCreateCategoryCommand();

        using var client = await CreateHttpClient();
        var response = await client.PostAsync("/api/category", new StringContent(JsonConvert.SerializeObject(createCategoryCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [DataTestMethod]
    public static CreateCategoryCommand GenerateCreateCategoryCommand()
    {
        return new Faker<CreateCategoryCommand>()
                     .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                     .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                     .Generate();
    }
}
