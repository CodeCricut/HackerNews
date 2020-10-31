using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Queries.GetArticle;
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

namespace Application.IntegrationTests.Articles.Queries.GetArticle
{
	public class GetArticleQueryTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldReturnArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetArticleHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetArticleModel sutResult = await sut.Handle(new GetArticleQuery(article.Id), new CancellationToken(false));


			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(article.Title, sutResult.Title);
			Assert.Equal(article.Text, sutResult.Text);
			Assert.Equal(article.Type, sutResult.Type);
			Assert.Equal(article.Id, sutResult.Id);
		}
	}
}
