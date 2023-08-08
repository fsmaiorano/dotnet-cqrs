using Bogus;
using Domain.Entities;

namespace IntegrationTest;

[TestClass]
public class GetAuthUserTest : Testing
{
    private readonly string email = "auth@test.com";
    private readonly string password = "123456";

    [TestInitialize]
    public async Task TestInitialize()
    {
        var userEntity = new Faker<UserEntity>()
                         .RuleFor(x => x.Name, f => f.Person.FirstName)
                         .RuleFor(x => x.Email, f => email)
                         .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                         .RuleFor(x => x.Slug, f => f.Person.UserName)
                         .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                         .RuleFor(x => x.PasswordHash, f => password)
                         .Generate();

        await AddAsync(userEntity);
    }

    // [TestMethod]
    // public async Task ShouldGenerateToken()
    // [
    //     var query = new GetAuthUserQuery()
    // {
    //     Email = "  
    // ]
}
