using System.Text;
using Application.UseCases.User.Commands.UpdateUser;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.User.Commands;

[TestClass]
public class UpdateUserTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateUserUseCase()
    {
        var createUserCommand = CreateUserTest.GenerateCreateUserCommand();

        var createdUserId = await SendAsync(createUserCommand);

        var user = await FindAsync<UserEntity>(createdUserId);

        Assert.IsNotNull(user);
        Assert.IsTrue(user.Id > 0);
        Assert.IsTrue(user.Name == createUserCommand.Name);

        user.Name = $"updated_{user.Name}";

        await SendAsync(new UpdateUserCommand
        {
            Id = user.Id,
            Name = user.Name,
        });

        user = await FindAsync<UserEntity>(createdUserId);

        Assert.IsNotNull(user);
        Assert.IsTrue(user.Name == $"updated_{createUserCommand.Name}");
    }

    [TestMethod]
    public async Task ShouldUpdateUserController()
    {
        var createUserCommand = CreateUserTest.GenerateCreateUserCommand();

        var createdUserId = await SendAsync(createUserCommand);

        var user = await FindAsync<UserEntity>(createdUserId);

        Assert.IsNotNull(user);
        Assert.IsTrue(user.Id > 0);
        Assert.IsTrue(user.Name == createUserCommand.Name);

        var UpdateTagCommand = new UpdateUserCommand
        {
            Id = user.Id,
            Name = $"updated_{user.Name}"
        };

        using var client = await CreateHttpClient();
        var response = await client.PutAsync($"/api/user?id={user.Id}", new StringContent(JsonConvert.SerializeObject(UpdateTagCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);

        user = await FindAsync<UserEntity>(createdUserId);

        Assert.IsNotNull(user);
        Assert.IsTrue(user.Name == $"updated_{createUserCommand.Name}");
    }
}
