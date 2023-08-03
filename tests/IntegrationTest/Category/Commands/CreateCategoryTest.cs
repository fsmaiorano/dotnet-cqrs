using Application.UseCases.Category.Commands.CreateCategory;
using Bogus;

namespace IntegrationTest.Category.Commands;

[TestClass]
public class CreateCategoryTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldCreateCategory()
    {
        var createCategoryCommand = GenerateCreateCategoryCommand();

        var createdCategoryId = await SendAsync(createCategoryCommand);
        Assert.IsNotNull(createdCategoryId);
        Assert.IsTrue(createdCategoryId > 0);
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
