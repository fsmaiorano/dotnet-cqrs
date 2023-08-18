using System.Text;
using Application.UseCases.Post.Commands.UpdatePost;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Post.Commands;

[TestClass]
public class UpdatePostTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdatePostUseCase()
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

    [TestMethod]
    public async Task ShouldUpdatePostWebApi()
    {
        var createPostCommand = await CreatePostTest.GenerateCreatePostCommand();

        var createdPostId = await SendAsync(createPostCommand);
        Assert.IsNotNull(createdPostId);
        Assert.IsTrue(createdPostId > 0);

        var updatedPost = new UpdatePostCommand
        {
            Id = createdPostId,
            Title = $"updated_{createPostCommand.Title}",
        };

        using var client = await CreateHttpClient();
        var response = await client.PutAsync($"/api/post?id={updatedPost.Id}", new StringContent(JsonConvert.SerializeObject(updatedPost), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);

        var post = await FindAsync<PostEntity>(createdPostId);

        Assert.IsNotNull(post);
        Assert.IsTrue(post.Title == $"updated_{createPostCommand.Title}");
    }
}
