using System.Text;
using Application.UseCases.Tag.Commands.UpdateTag;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Tag.Commands;

[TestClass]
public class UpdateTagTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateTagUseCase()
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

    [TestMethod]
    public async Task ShouldUpdateTagController()
    {
        var createTagCommand = CreateTagTest.GenerateCreateTagCommand();

        var createdTagId = await SendAsync(createTagCommand);

        var tag = await FindAsync<TagEntity>(createdTagId);

        Assert.IsNotNull(tag);
        Assert.IsTrue(tag.Id > 0);
        Assert.IsTrue(tag.Name == createTagCommand.Name);

        var UpdateTagCommand = new UpdateTagCommand
        {
            Id = tag.Id,
            Name = $"updated_{tag.Name}"
        };

        using var client = await CreateHttpClient();
        var response = await client.PutAsync($"/api/tag?id={tag.Id}", new StringContent(JsonConvert.SerializeObject(UpdateTagCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);

        tag = await FindAsync<TagEntity>(createdTagId);

        Assert.IsNotNull(tag);
        Assert.IsTrue(tag.Name == $"updated_{createTagCommand.Name}");
    }
}
