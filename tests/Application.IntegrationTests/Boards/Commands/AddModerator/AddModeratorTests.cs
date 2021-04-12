using Application.IntegrationTests.Common;
using HackerNews.Application.Boards.Commands.AddModerator;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Boards.Commands.AddModerator
{
	public class AddModeratorTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldAddModerator()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			Assert.DoesNotContain(user.Id, board.Moderators.Select(bm => bm.UserId));

			var sut = new AddModeratorHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetBoardModel sutResult = await sut.Handle(new AddModeratorCommand(board.Id, user.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedBoard = await unitOfWork.Boards.GetEntityAsync(sutResult.Id);

			Assert.Contains(user.Id, updatedBoard.Moderators.Select(bm => bm.UserId));
		}

		[Fact]
		public async Task ShouldRemoveModerator()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var addModHandler = new AddModeratorHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);
			await addModHandler.Handle(new AddModeratorCommand(board.Id, user.Id), new CancellationToken());

			Assert.Contains(user.Id, board.Moderators.Select(boardUserMod => boardUserMod.UserId));

			var sut = new AddModeratorHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetBoardModel sutResult = await sut.Handle(new AddModeratorCommand(board.Id, user.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedBoard = await unitOfWork.Boards.GetEntityAsync(sutResult.Id);

			Assert.DoesNotContain(user.Id, updatedBoard.Moderators.Select(bm => bm.UserId));
		}
	}
}
