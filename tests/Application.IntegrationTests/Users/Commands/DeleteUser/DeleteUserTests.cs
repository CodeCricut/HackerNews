using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Commands.DeleteUser;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
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

			var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
			var mediator = scope.ServiceProvider.GetService<IMediator>();
			var mapper = scope.ServiceProvider.GetService<IMapper>();
			var currentUserServiceMock = new Mock<ICurrentUserService>();

			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

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
