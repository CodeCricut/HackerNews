using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.UpdateArticle;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.UpdateArticle
{
	public class UpdateArticleCommandTest : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldUpdateArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var updateArticleModel = new PostArticleModel
			{
				BoardId = -1, // make sure board id can't be updated
				Text = "updated text",
				Title = "updated title",
				Type = "meta"
			};

			var sut = new UpdateArticleHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetArticleModel sutResult = await sut.Handle(new UpdateArticleCommand(article.Id, updateArticleModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedArticle = await unitOfWork.Articles.GetEntityAsync(sutResult.Id);
			Assert.NotNull(updatedArticle);
			Assert.False(updatedArticle.Deleted);
			Assert.Equal(updatedArticle.UserId, user.Id);
			Assert.Equal(updatedArticle.BoardId, board.Id);

			Assert.Equal(updatedArticle.Text, updateArticleModel.Text);
			Assert.Equal(updatedArticle.Title, updateArticleModel.Title);
		}
	}
}
