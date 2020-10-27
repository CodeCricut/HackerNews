using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Commands.SaveArticleToUser;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();


			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);


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
