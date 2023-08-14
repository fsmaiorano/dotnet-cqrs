using Application.UseCases.Category.Commands.CreateCategory;
using Application.UseCases.Post.Commands.CreatePost;
using Application.UseCases.Post.Commands.UpdatePost;
using Application.UseCases.Tag.Commands.CreateTag;
using Application.UseCases.User.Commands.CreateUser;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.Post.Commands;

[TestClass]
public class PublishPostTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldPublishPost()
    {
        var createPostCommand = await GenerateCreatePostCommand();

        var createdPostId = await SendAsync(createPostCommand);
        Assert.IsNotNull(createdPostId);
        Assert.IsTrue(createdPostId > 0);

        var publishPostCommand = new PublishPostCommand()
        {
            Id = createdPostId,
            IsPublished = true
        };

        await SendAsync(publishPostCommand);

        var storedPost = await FindAsync<PostEntity>(createdPostId);
        Assert.IsTrue(storedPost!.IsPublished);
        Assert.IsNotNull(storedPost.UpdateDate);
    }

    [DataTestMethod]
    public static async Task<CreatePostCommand> GenerateCreatePostCommand()
    {
        var createUserCommand = new Faker<CreateUserCommand>()
                       .RuleFor(x => x.Name, f => f.Person.FirstName)
                       .RuleFor(x => x.Email, f => f.Person.Email)
                       .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                       .RuleFor(x => x.Slug, f => f.Person.UserName)
                       .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                       .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
                       .Generate();

        var createTagCommand = new Faker<CreateTagCommand>()
                       .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                       .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                       .Generate();

        var createCategoryCommand = new Faker<CreateCategoryCommand>()
                       .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                       .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                       .Generate();

        var createdTagId = await SendAsync(createTagCommand);
        var createdUserId = await SendAsync(createUserCommand);
        var createdCategoryId = await SendAsync(createCategoryCommand);

        var createPostCommand = new Faker<CreatePostCommand>()
                      .RuleFor(x => x.Title, f => f.Lorem.Word())
                      .RuleFor(x => x.Summary, f => f.Lorem.Word())
                      .RuleFor(x => x.Body, f => f.Lorem.Paragraphs(10))
                      .RuleFor(x => x.Slug, f => f.Commerce.Categories(1)[0])
                      .Generate();

        createPostCommand.AuthorId = createdUserId;
        createPostCommand.CategoryId = createdCategoryId;
        createPostCommand.Tags = new List<int> { createdTagId };

        return createPostCommand;
    }
}
