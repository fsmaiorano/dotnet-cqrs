using Application.UseCases.Post.Commands.UpdatePost;
using Domain.Entities;

namespace IntegrationTest.Post.Commands;

[TestClass]
public class UpdatePostTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdatePost()
    {
        var createPostCommand = await CreatePostTest.GenerateCreatePostCommand();

        var createdPostId = await SendAsync(createPostCommand);
        Assert.IsNotNull(createdPostId);
        Assert.IsTrue(createdPostId > 0);

        var post = await FindAsync<PostEntity>(createdPostId);

        post!.Title = $"updated_{post.Title}";

        await SendAsync(new UpdatePostCommand
        {
            Id = post.Id,
            Title = post.Title,
        });

        post = await FindAsync<PostEntity>(createdPostId);

        Assert.IsNotNull(post);
        Assert.IsTrue(post.Title == $"updated_{createPostCommand.Title}");
    }
}
