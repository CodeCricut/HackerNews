using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Queries.GetArticle
{
	public class GetArticleQueryTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var sut = new GetArticleHandler(deletedArticleValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetArticleModel sutResult = await sut.Handle(new GetArticleQuery(article.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(article.Title, sutResult.Title);
			Assert.Equal(article.Text, sutResult.Text);
			Assert.Equal(article.Type, sutResult.Type);
			Assert.Equal(article.Id, sutResult.Id);
		}
	}
}
