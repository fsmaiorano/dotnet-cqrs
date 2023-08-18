using Application.UseCases.Post.Commands.DeletePost;
using Domain.Entities;

namespace IntegrationTest.Post.Commands;

[TestClass]
public class DeletePostTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldDeletePostUseCase()
    {
        var createPostCommand = await CreatePostTest.GenerateCreatePostCommand();

        var createdPostId = await SendAsync(createPostCommand);
        Assert.IsNotNull(createdPostId);
        Assert.IsTrue(createdPostId > 0);

        await SendAsync(new DeletePostCommand(createdPostId));

        var postEntity = await FindAsync<PostEntity>(createdPostId);
        Assert.IsNull(postEntity);
    }

    [TestMethod]
    public async Task ShouldDeletePostWebApi()
    {
        var createPostCommand = await CreatePostTest.GenerateCreatePostCommand();

        var createdPostId = await SendAsync(createPostCommand);
        Assert.IsNotNull(createdPostId);
        Assert.IsTrue(createdPostId > 0);

        using var client = await CreateHttpClient();
        var response = await client.DeleteAsync($"/api/post?id={createdPostId}");

        var postEntity = await FindAsync<PostEntity>(createdPostId);
        Assert.IsNull(postEntity);
    }
}
