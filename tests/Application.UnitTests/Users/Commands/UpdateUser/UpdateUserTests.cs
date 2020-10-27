using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Commands.UpdateUser;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
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

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var updateUserModel = new UpdateUserModel
			{
				FirstName = "updated first name",
				LastName = "updated last name",
				Password = "updated password"
			};

			var sut = new UpdateUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new UpdateUserCommand(updateUserModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedUser = await unitOfWork.Users.GetEntityAsync(sutResult.Id);
			Assert.NotNull(updatedUser);
			Assert.False(updatedUser.Deleted);
			Assert.Equal(updatedUser.Username, user.Username);

			Assert.Equal(updatedUser.FirstName, updateUserModel.FirstName);
			Assert.Equal(updatedUser.LastName, updateUserModel.LastName);
			Assert.Equal(updatedUser.Password, updateUserModel.Password);
		}
	}
}
