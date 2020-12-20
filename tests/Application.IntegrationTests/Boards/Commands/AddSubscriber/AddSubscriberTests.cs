using Application.IntegrationTests.Common;
using HackerNews.Application.Boards.Commands.AddSubscriber;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.DependencyInjection;
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
