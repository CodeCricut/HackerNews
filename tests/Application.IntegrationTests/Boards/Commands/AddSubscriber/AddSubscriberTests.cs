using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Boards.Commands.AddSubscriber;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Boards.Commands.AddSubscriber
{
	public class AddSubscriberTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldAddModerator()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			Assert.DoesNotContain(user.Id, board.Subscribers.Select(bs => bs.UserId));

			var sut = new AddSubscriberHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetBoardModel sutResult = await sut.Handle(new AddSubscriberCommand(board.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedBoard = await unitOfWork.Boards.GetEntityAsync(sutResult.Id);

			Assert.Contains(user.Id, updatedBoard.Subscribers.Select(bs => bs.UserId));
		}
	}
}
