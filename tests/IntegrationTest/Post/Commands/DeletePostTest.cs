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
    public async Task ShouldDeletePost()
    {
        var createPostCommand = await CreatePostTest.GenerateCreatePostCommand();

        var createdPostId = await SendAsync(createPostCommand);
        Assert.IsNotNull(createdPostId);
        Assert.IsTrue(createdPostId > 0);

        await SendAsync(new DeletePostCommand(createdPostId));

        var category = await FindAsync<PostEntity>(createdPostId);
        Assert.IsNull(category);
    }
}
