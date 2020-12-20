using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Queries.GetArticlesWithPagination
{
	public class GetArticlesWithPaginationTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetArticlesByIds()
		{
			using var scope = Factory.Services.CreateScope();

			await AddArticlesToQueryFrom();

			var sut = new GetArticlesWithPaginationQueryHandler(deletedArticleValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var pagingParams = new PagingParams
			{
				PageSize = (int)Math.Ceiling((decimal)articles.Count() / 2),
				PageNumber = 1
			};

			// Act
			PaginatedList<GetArticleModel> sutResult = await sut.Handle(
				new GetArticlesWithPaginationQuery(pagingParams), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(pagingParams.PageSize, sutResult.PageSize);
			Assert.Equal(pagingParams.PageSize, sutResult.Items.Count());
			if (pagingParams.PageSize < articles.Count()) Assert.True(sutResult.HasNextPage);
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
