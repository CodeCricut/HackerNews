using Application.IntegrationTests.Common;
using HackerNews.Application.Boards.Queries.GetBoardByTitle;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Boards.Queries
{
	public class GetBoardByTitleTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetValidBoard()
		{
			using var scope = Factory.Services.CreateScope();

			var sut = new GetBoardByTitleHandler(deletedBoardValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			var sutResult = await sut.Handle(
				new GetBoardByTitleQuery(board.Title), new System.Threading.CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);
			Assert.Equal(board.Title, sutResult.Title);
			Assert.Equal(board.Id, sutResult.Id);
		}
	}
}
