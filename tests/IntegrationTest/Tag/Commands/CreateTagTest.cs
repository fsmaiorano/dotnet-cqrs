using System.Text;
using Application.UseCases.Tag.Commands.CreateTag;
using Bogus;
using Newtonsoft.Json;

namespace IntegrationTest.Tag.Commands;

[TestClass]
public class CreateTagTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldCreateTagUseCase()
    {
        var createTagCommand = GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);
        Assert.IsNotNull(createdTagId);
        Assert.IsTrue(createdTagId > 0);
    }

    [TestMethod]
    public async Task ShouldCreateTagWebApi()
    {
        var createTagCommand = GenerateCreateTagCommand();

        using var client = await CreateHttpClient();
        var response = await client.PostAsync("/api/tag", new StringContent(JsonConvert.SerializeObject(createTagCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);
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
