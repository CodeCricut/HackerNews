using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Articles.Commands.UpdateArticle;
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

namespace Application.IntegrationTests.Articles.Commands.UpdateArticle
{
	public class UpdateArticleCommandTest : AppIntegrationTest
	{
		//public UpdateArticleCommandTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
		//{
		//}

		[Fact]
		public async Task ShouldUpdateArticle()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var updateArticleModel = new PostArticleModel
			{
				BoardId = -1, // make sure board id can't be updated
				Text = "updated text",
				Title = "updated title",
				Type = "meta"
			};

			var sut = new UpdateArticleHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetArticleModel sutResult = await sut.Handle(new UpdateArticleCommand(article.Id, updateArticleModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedArticle = await unitOfWork.Articles.GetEntityAsync(sutResult.Id);
			Assert.NotNull(updatedArticle);
			Assert.False(updatedArticle.Deleted);
			Assert.Equal(updatedArticle.UserId, user.Id);
			Assert.Equal(updatedArticle.BoardId, board.Id);

			Assert.Equal(updatedArticle.Text, updateArticleModel.Text);
			Assert.Equal(updatedArticle.Title, updateArticleModel.Title);
		}
	}
}
