using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.AddArticles
{
	public class AddArticlesCommandTest : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldAddArticles()
		{
			using var scope = Factory.Services.CreateScope();

			var postArticleModels = new List<PostArticleModel>
			{
				new PostArticleModel
				{
					BoardId = board.Id,
					Text = "text 1",
					Title = "title 1",
					Type = "meta"
				},
				new PostArticleModel
				{
					BoardId = board.Id,
					Text = "text 2",
					Title = "title 2",
					Type = "meta"
				},
			};

			var originalArticleCount = unitOfWork.Articles.GetEntitiesAsync().Result.Count();

			var sut = new AddArticlesCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			IEnumerable<GetArticleModel> sutResult = await sut.Handle(new AddArticlesCommand(postArticleModels), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);
			Assert.Equal(sutResult.Count(), postArticleModels.Count());

			var articles = await unitOfWork.Articles.GetEntitiesAsync();
			Assert.Equal(articles.Count(), originalArticleCount + postArticleModels.Count());
		}
	}
}
