using Application.UseCases.Category.Commands.CreateCategory;
using Application.UseCases.Post.Commands.CreatePost;
using Application.UseCases.Post.Queries.GetPost;
using Application.UseCases.Tag.Commands.CreateTag;
using Application.UseCases.User.Commands.CreateUser;
using Bogus;

namespace IntegrationTest.Post.Queries;

[TestClass]
public class GetPostTest : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
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

        await SendAsync(createPostCommand);
    }

    [TestMethod]
    public async Task ShouldReturnAllPost()
    {
        var query = new GetPostQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }


    [TestMethod]
    public async Task ShouldReturnPaginatedListWithPost()
    {
        var query = new GetPostWithPaginationQuery() { PageSize = 9999, PageNumber = 1 };
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }
}
