using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.SaveArticleToUser;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Commands.SaveArticleToUser
{
	public class SaveArticleToUserTests : AppIntegrationTest
	{

		[Fact]
		public async Task ShouldSaveArticleToUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var sut = new SaveArticleToUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new SaveArticleToUserCommand(article.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedUser = await unitOfWork.Users.GetEntityAsync(user.Id);
			Assert.Contains(article.Id, updatedUser.SavedArticles.Select(sa => sa.ArticleId));
		}
	}
}
