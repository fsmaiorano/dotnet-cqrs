using Application.UseCases.User.Queries.GetUser;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.User.Queries;

[TestClass]
public class GetAuthUserTest : Testing
{
    private readonly string email = "auth@test.com";
    private readonly string password = "123456";

    public GetAuthUserTest()
    {
    }

    [TestInitialize]
    public async Task TestInitialize()
    {
        var userEntity = new Faker<UserEntity>()
                         .CustomInstantiator(f => new UserEntity(f.Person.UserName, f.Person.Email, f.Random.Hash(64)))
                         .RuleFor(x => x.Name, f => f.Person.FirstName)
                         .RuleFor(x => x.Email, f => email)
                         .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                         .RuleFor(x => x.Slug, f => f.Person.UserName)
                         .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                         .RuleFor(x => x.PasswordHash, f => password)
                         .Generate();

        await AddAsync(userEntity);
    }

    [TestMethod]
    public async Task ShouldGenerateToken()
    {
        var query = new GetAuthUserQuery() { Email = email, PasswordHash = password };

        var storedUser = await SendAsync(query);

        Assert.IsNotNull(storedUser);

        var userToken = await _authService.GenerateToken(storedUser!);

        Assert.IsNotNull(userToken);
    }


    [TestMethod]
    public async Task ShouldGenerateAnValidToken()
    {
        var query = new GetAuthUserQuery() { Email = email, PasswordHash = password };

        var storedUser = await SendAsync(query);

        Assert.IsNotNull(storedUser);

        var userToken = await _authService.GenerateToken(storedUser!);

        Assert.IsNotNull(userToken);

        var isValidToken = await _authService.ValidateToken(userToken!);
        Assert.IsTrue(isValidToken);
    }
}
