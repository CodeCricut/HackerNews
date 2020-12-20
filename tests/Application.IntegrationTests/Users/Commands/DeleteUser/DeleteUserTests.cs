using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.DeleteUser;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Commands.DeleteUser
{
	public class DeleteUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldSoftDeleteUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			Assert.False(user.Deleted);

			var sut = new DeleteCurrentUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			bool result = await sut.Handle(new DeleteCurrentUserCommand(), new System.Threading.CancellationToken(false));

			// Assert
			Assert.True(result);

			var deletedUser = await unitOfWork.Users.GetEntityAsync(user.Id);
			Assert.NotNull(deletedUser);
			Assert.True(deletedUser.Deleted);
		}
	}
}
