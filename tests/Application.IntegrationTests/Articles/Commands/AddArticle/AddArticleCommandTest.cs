using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.AddArticle
{

	// WebApplicationFactory<TEntryPoint> is used to create a TestServer for the integration tests.
	// Test classes implement a class fixture interface (IClassFixture) to indicate the class contains tests 
	// and provide shared object instances across the tests in the class.
	public class AddArticleCommandTest : AppIntegrationTest
	{
		//public AddArticleCommandTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
		//{
		//}

		[Fact]
		public async Task ShouldCreateArticle()
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

			var postArticleModel = new PostArticleModel
			{
				BoardId = board.Id,
				Text = "text",
				Title = "title",
				Type = "meta"
			};

			var sut = new AddArticleCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetArticleModel sutResult = await sut.Handle(new AddArticleCommand(postArticleModel), new CancellationToken(false));


			// Assert
			Assert.NotNull(sutResult);

			var addedArticle = await unitOfWork.Articles.GetEntityAsync(sutResult.Id);
			Assert.NotNull(addedArticle);
			Assert.False(addedArticle.Deleted);
			Assert.Equal(addedArticle.UserId, user.Id);
			Assert.Equal(addedArticle.BoardId, board.Id);
		}
	}
}
