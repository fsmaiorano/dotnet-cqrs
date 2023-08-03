using Application.UseCases.Tag.Commands.CreateTag;
using Application.UseCases.Tag.Commands.UpdateTag;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Tag.Commands;

[TestClass]
public class UpdateTagTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateTag()
    {
        var createTagCommand = CreateTagTest.GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);

        var tag = await FindAsync<TagEntity>(createdTagId);

        Assert.IsNotNull(tag);
        Assert.IsTrue(tag.Id > 0);
        Assert.IsTrue(tag.Name == createTagCommand.Name);

        tag.Name = $"updated_{tag.Name}";

        await SendAsync(new UpdateTagCommand
        {
            Id = tag.Id,
            Name = tag.Name,
        });

        tag = await FindAsync<TagEntity>(createdTagId);

        Assert.IsNotNull(tag);
        Assert.IsTrue(tag.Name == $"updated_{createTagCommand.Name}");
    }
}
