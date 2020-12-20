using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.DeleteArticle;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.DeleteArticle
{
	public class DeleteArticleCommandTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldSoftDeleteArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			Assert.False(article.Deleted);
			var articleId = article.Id;

			var sut = new DeleteArticleHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			bool result = await sut.Handle(new DeleteArticleCommand(articleId), new System.Threading.CancellationToken(false));

			// Assert
			Assert.True(result);

			var deletedArticle = await unitOfWork.Articles.GetEntityAsync(articleId);
			Assert.NotNull(deletedArticle);
			Assert.True(deletedArticle.Deleted);
		}
	}
}
