using Application.UseCases.Tag.Commands.CreateTag;
using Application.UseCases.Tag.Commands.DeleteTag;
using Bogus;
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
    public async Task ShouldDeleteTag()
    {
        var createTagCommand = CreateTagTest.GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);
        Assert.IsNotNull(createdTagId);
        Assert.IsTrue(createdTagId > 0);

        await SendAsync(new DeleteTagCommand(createdTagId));

        var tag = await FindAsync<TagEntity>(createdTagId);
        Assert.IsNull(tag);
    }
}
