using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.DependencyInjection;
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
		[Fact]
		public async Task ShouldCreateArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

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
