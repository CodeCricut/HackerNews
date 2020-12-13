using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Boards.Queries.GetBoardByTitle;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
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

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetBoardByTitleHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

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
