using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.UpdateUser;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Commands.UpdateUser
{
	public class UpdateUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldUpdateUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var updateUserModel = new UpdateUserModel
			{
				FirstName = "updated first name",
				LastName = "updated last name",
			};

			var sut = new UpdateUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new UpdateUserCommand(updateUserModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedUser = await unitOfWork.Users.GetEntityAsync(sutResult.Id);
			Assert.NotNull(updatedUser);
			Assert.False(updatedUser.Deleted);
			Assert.Equal(updatedUser.UserName, user.UserName);

			Assert.Equal(updatedUser.FirstName, updateUserModel.FirstName);
			Assert.Equal(updatedUser.LastName, updateUserModel.LastName);
		}
	}
}
