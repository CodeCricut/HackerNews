using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			// Add articles to query from.
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


			var sut = new GetArticlesWithPaginationQueryHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var allArticles = await unitOfWork.Articles.GetEntitiesAsync();
			var pagingParams = new PagingParams
			{
				PageSize = (int)Math.Ceiling((decimal)allArticles.Count() / 2),
				PageNumber = 1
			};

			// Act
			PaginatedList<GetArticleModel> sutResult = await sut.Handle(
				new GetArticlesWithPaginationQuery(pagingParams), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(pagingParams.PageSize, sutResult.PageSize);
			Assert.Equal(pagingParams.PageSize, sutResult.Items.Count());
			if (pagingParams.PageSize < allArticles.Count()) Assert.True(sutResult.HasNextPage);
		}
	}
}
