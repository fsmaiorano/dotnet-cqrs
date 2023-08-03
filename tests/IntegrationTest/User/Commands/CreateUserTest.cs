using Application.UseCases.User.Commands.CreateUser;
using Bogus;

namespace IntegrationTest.User.Commands;

[TestClass]
public class CreateUserTest : Testing
{
    [TestMethod]
    public async Task ShouldCreateUser()
    {
        var createUserCommand = new Faker<CreateUserCommand>()
                        .RuleFor(x => x.Name, f => f.Person.FirstName)
                        .RuleFor(x => x.Email, f => f.Person.Email)
                        .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                        .RuleFor(x => x.Slug, f => f.Person.UserName)
                        .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                        .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
                        .Generate();

        var createdUserId = await SendAsync(createUserCommand);
        Assert.IsNotNull(createdUserId);
        Assert.IsTrue(createdUserId > 0);
    }

    [DataTestMethod]
    public static CreateUserCommand GenerateCreateUserCommand()
    {
        return new Faker<CreateUserCommand>()
                        .RuleFor(x => x.Name, f => f.Person.FirstName)
                        .RuleFor(x => x.Email, f => f.Person.Email)
                        .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                        .RuleFor(x => x.Slug, f => f.Person.UserName)
                        .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                        .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
                        .Generate();
    }
}