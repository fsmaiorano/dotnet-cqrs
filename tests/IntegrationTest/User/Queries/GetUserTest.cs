using Application.UseCases.User.Queries.GetUser;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.User.Queries;

[TestClass]
public class GetUserTest : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        var userEntity = new Faker<UserEntity>()
                         .CustomInstantiator(f => new UserEntity(f.Person.UserName, f.Person.Email, f.Random.Hash(64)))
                         .RuleFor(x => x.Name, f => f.Person.FirstName)
                         .RuleFor(x => x.Email, f => f.Person.Email)
                         .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                         .RuleFor(x => x.Slug, f => f.Person.UserName)
                         .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                         .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
                         .Generate();

        await AddAsync(userEntity);

        userEntity = new Faker<UserEntity>()
                     .CustomInstantiator(f => new UserEntity(f.Person.UserName, f.Person.Email, f.Random.Hash(64)))
                     .RuleFor(x => x.Name, f => f.Person.FirstName)
                     .RuleFor(x => x.Email, f => f.Person.Email)
                     .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                     .RuleFor(x => x.Slug, f => f.Person.UserName)
                     .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                     .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
                     .Generate();

        await AddAsync(userEntity);
    }

    [TestMethod]
    public async Task ShouldReturnAllUsers()
    {
        var query = new GetUserQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }


    [TestMethod]
    public async Task ShouldReturnPaginatedListWithUsers()
    {
        var query = new GetUserWithPaginationQuery() { PageSize = 9999, PageNumber = 1};
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }
}
