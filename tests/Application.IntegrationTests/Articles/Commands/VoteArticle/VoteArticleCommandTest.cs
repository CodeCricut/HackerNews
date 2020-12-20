using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.VoteArticle;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.VoteArticle
{
	public class VoteArticleCommandTest : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldUpvoteArticleAndUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			Assert.Equal(article.UserId, user.Id);
			var originalArticleKarma = article.Karma;
			var originalUserKarma = user.Karma;

			var sut = new VoteArticleCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

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

			Assert.Equal(article.UserId, user.Id);
			var originalArticleKarma = article.Karma;
			var originalUserKarma = user.Karma;

			var sut = new VoteArticleCommandHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

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
