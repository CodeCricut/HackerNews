using Application.IntegrationTests.Common;
using HackerNews.Application.Articles.Commands.AddImage;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Application.IntegrationTests.Articles.Commands.AddImage
{
	public class AddArticleImageCommandTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldCreateAndAddImage()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			PostImageModel postImageModel = new PostImageModel
			{
				ContentType = "media/png",
				ImageData = new byte[0],
				ImageDescription = "image description",
				ImageTitle = "image title"
			};

			var sut = new AddArticleImageHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetArticleModel sutResult = await sut.Handle(new AddArticleImageCommand(postImageModel, article.Id), new CancellationToken(false));

			// Assert
			// Should return updated model.
			Assert.NotNull(sutResult);
			Assert.Equal(article.Id, sutResult.Id);

			// Should add references to image.
			Assert.True(0 < sutResult.AssociatedImageId);

			// Added image should be valid.
			Image image = await unitOfWork.Images.GetEntityAsync(sutResult.AssociatedImageId);
			Assert.NotNull(image);
			Assert.Equal(postImageModel.ImageTitle, image.ImageTitle);
			Assert.Equal(postImageModel.ImageData, image.ImageData);
			Assert.Equal(postImageModel.ContentType, image.ContentType);
			Assert.Equal(postImageModel.ImageDescription, image.ImageDescription);
		}
	}
}
