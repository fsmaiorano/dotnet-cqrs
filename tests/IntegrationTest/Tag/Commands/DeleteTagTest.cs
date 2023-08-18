using Application.UseCases.Tag.Commands.DeleteTag;
using Domain.Entities;

namespace IntegrationTest.Tag.Commands;

[TestClass]
public class DeleteTagTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldDeleteTagUseCase()
    {
        var createTagCommand = CreateTagTest.GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);
        Assert.IsNotNull(createdTagId);
        Assert.IsTrue(createdTagId > 0);

        await SendAsync(new DeleteTagCommand(createdTagId));

        var tag = await FindAsync<TagEntity>(createdTagId);
        Assert.IsNull(tag);
    }

    [TestMethod]
    public async Task ShouldDeleteTagWebApi()
    {
        var createTagCommand = CreateTagTest.GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);
        Assert.IsNotNull(createdTagId);
        Assert.IsTrue(createdTagId > 0);

        using var client = await CreateHttpClient();
        var response = await client.DeleteAsync($"/api/tag?id={createdTagId}");

        var tag = await FindAsync<TagEntity>(createdTagId);
        Assert.IsNull(tag);
    }
}
