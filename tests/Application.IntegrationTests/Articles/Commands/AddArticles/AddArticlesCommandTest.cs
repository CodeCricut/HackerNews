using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.AddArticles
{
	public class AddArticlesCommandTest : AppIntegrationTest
	{
		//public AddArticlesCommandTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
		//{
		//}

		[Fact]
		public async Task ShouldAddArticles()
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
					Text = "text 1",
					Title = "title 1",
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
