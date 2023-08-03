using Application.UseCases.Tag.Commands.CreateTag;
using Bogus;

namespace IntegrationTest.Tag.Commands;

[TestClass]
public class CreateTagTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldCreateTag()
    {
        var createTagCommand = GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);
        Assert.IsNotNull(createdTagId);
        Assert.IsTrue(createdTagId > 0);
    }

    [DataTestMethod]
    public static CreateTagCommand GenerateCreateTagCommand()
    {
        return new Faker<CreateTagCommand>()
                     .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                     .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                     .Generate();
    }
}
