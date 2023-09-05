using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Commands.DeleteUser;
using Bogus;
using Domain.Entities;

namespace IntegrationTest.User.Commands;

[TestClass]
public class DeleteUserTest : Testing
{
    [TestMethod]
    public async Task ShouldDeleteUserUseCase()
    {
        var createUserCommand = CreateUserTest.GenerateCreateUserCommand();

        var createdUserId = await SendAsync(createUserCommand);

        await SendAsync(new DeleteUserCommand(createdUserId));

        var user = await FindAsync<UserEntity>(createdUserId);
        Assert.IsNull(user);
    }

     [TestMethod]
    public async Task ShouldDeleteUserController()
    {
        var createUserCommand = CreateUserTest.GenerateCreateUserCommand();

        var createdUserId = await SendAsync(createUserCommand);

        using var client = await CreateHttpClient();
        var response = await client.DeleteAsync($"/api/user?id={createdUserId}");

        var user = await FindAsync<UserEntity>(createdUserId);
        Assert.IsNull(user);
    }
}
