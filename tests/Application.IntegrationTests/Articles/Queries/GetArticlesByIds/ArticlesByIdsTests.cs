using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Application.Articles.Queries.GetArticlesByIds;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Queries.GetArticlesByIds
{
	public class ArticlesByIdsTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetArticlesByIds()
		{
			using var scope = Factory.Services.CreateScope();

			await AddArticlesToQueryFrom();


			var oddArticles = articles.Where(a => a.Id % 2 == 1);
			deletedArticleValidatorMock.Setup(m => m.ValidateEntityQuerable(It.IsAny<IQueryable<Article>>(), It.IsAny<DeletedEntityPolicy>())).Returns(oddArticles);

			var sut = new GetArticlesByIdsQueryHandler(deletedArticleValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			PaginatedList<GetArticleModel> sutResult = await sut.Handle(
				new GetArticlesByIdsQuery(oddArticles.Select(a => a.Id),
				new PagingParams(1, 20)), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			// Ids should all be odd
			Assert.True(sutResult.Items.All(a => a.Id % 2 == 1));
		}

		private async Task AddArticlesToQueryFrom()
		{
			var postArticleModels = new List<PostArticleModel>
			{
				new PostArticleModel
				{
					BoardId = board.Id,
					Text = "postedArticle1",
					Title = "postedArticle1",
					Type = "meta"
				},
				new PostArticleModel
				{
					BoardId = board.Id,
					Text = "postedArticle2",
					Title = "postedArticle2",
					Type = "meta"
				}
			};
			await new AddArticlesCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object)
				.Handle(new AddArticlesCommand(postArticleModels), new CancellationToken(false));
		}
	}
}
