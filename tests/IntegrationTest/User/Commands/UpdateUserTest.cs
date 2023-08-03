using Application.UseCases.User.Commands.UpdateUser;
using Domain.Entities;

namespace IntegrationTest.User.Commands;

[TestClass]
public class UpdateUserTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateUser()
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
}
