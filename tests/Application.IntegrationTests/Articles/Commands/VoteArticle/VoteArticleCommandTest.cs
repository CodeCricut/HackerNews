using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Commands.VoteArticle;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.VoteArticle
{
	public class VoteArticleCommandTest : AppIntegrationTest
	{
		//public VoteArticleCommandTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
		//{
		//}

		[Fact]
		public async Task ShouldUpvoteArticleAndUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var sut = new VoteArticleCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			Assert.Equal(article.UserId, user.Id);
			var originalArticleKarma = article.Karma;
			var originalUserKarma = user.Karma;

			// Act
			GetArticleModel sutResult = await sut.Handle(new VoteArticleCommand(article.Id, true), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var upvotedArticle = await unitOfWork.Articles.GetEntityAsync(sutResult.Id);
			Assert.NotNull(upvotedArticle);

			Assert.Equal(originalArticleKarma + 1, upvotedArticle.Karma);
			Assert.Equal(originalUserKarma + 1, user.Karma);
		}

		[Fact]
		public async Task ShouldDownvoteArticleAndUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var sut = new VoteArticleCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			Assert.Equal(article.UserId, user.Id);
			var originalArticleKarma = article.Karma;
			var originalUserKarma = user.Karma;

			// Act
			GetArticleModel sutResult = await sut.Handle(new VoteArticleCommand(article.Id, false), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var downvotedArticle = await unitOfWork.Articles.GetEntityAsync(sutResult.Id);
			Assert.NotNull(downvotedArticle);

			Assert.Equal(originalArticleKarma - 1, downvotedArticle.Karma);
			Assert.Equal(originalUserKarma - 1, user.Karma);
		}
	}
}
