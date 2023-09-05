using System.Text;
using Application.UseCases.User.Commands.CreateUser;
using Bogus;
using Newtonsoft.Json;

namespace IntegrationTest.User.Commands;

[TestClass]
public class CreateUserTest : Testing
{
    [TestMethod]
    public async Task ShouldCreateUserUseCase()
    {
        var createUserCommand = GenerateCreateUserCommand();

        var createdUserId = await SendAsync(createUserCommand);
        Assert.IsNotNull(createdUserId);
        Assert.IsTrue(createdUserId > 0);
    }

    [TestMethod]
    public async Task ShouldCreateUserController()
    {
        var createUserCommand = GenerateCreateUserCommand();

        using var client = await CreateHttpClient();
        var response = await client.PostAsync("/api/user",
                                               new StringContent(
                                                JsonConvert.SerializeObject(createUserCommand),
                                                Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);
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
