using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Commands.DeleteArticle;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Articles.Commands.DeleteArticle
{
	public class DeleteArticleCommandTests : AppIntegrationTest
	{
		//public DeleteArticleCommandTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
		//{
		//}

		[Fact]
		public async Task ShouldSoftDeleteArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
			var mediator = scope.ServiceProvider.GetService<IMediator>();
			var mapper = scope.ServiceProvider.GetService<IMapper>();
			var currentUserServiceMock = new Mock<ICurrentUserService>();

			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			Assert.False(article.Deleted);
			var articleId = article.Id;

			var sut = new DeleteArticleHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			bool result = await sut.Handle(new DeleteArticleCommand(articleId), new System.Threading.CancellationToken(false));

			// Assert
			Assert.True(result);

			var deletedArticle = await unitOfWork.Articles.GetEntityAsync(articleId);
			Assert.NotNull(deletedArticle);
			Assert.True(deletedArticle.Deleted);
		}
	}
}
