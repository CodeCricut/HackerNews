using HackerNews.Api.Controllers;
using HackerNews.Api.Helpers;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models;
using HackerNews.EF;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.UnitTests.Controllers
{
	public class ArticleControllerShould
	{
		public Mock<EntityRepository<Article>> _mockRepo;

		public Mock<IEntityHelper<Article, PostArticleModel, GetArticleModel>> _mockHelper;
		public Mock<IVoteableEntityHelper<Article>> _mockArticleVoter;

		public ArticlesController _controller;

		public PostArticleModel _postArticleModel;
		public List<PostArticleModel> _postArticleModels;

		public GetArticleModel _getArticleModel;
		public List<GetArticleModel> _getArticleModels;

		public ArticleControllerShould()
		{
			_mockRepo = new Mock<EntityRepository<Article>>(null);

			_mockHelper = new Mock<IEntityHelper<Article, PostArticleModel, GetArticleModel>>();
			_mockArticleVoter = new Mock<IVoteableEntityHelper<Article>>();

			_controller = new ArticlesController(_mockHelper.Object, _mockArticleVoter.Object);

			_postArticleModel = new PostArticleModel()
			{
				//AuthorName = "Valid name",
				Text = "valid text",
				Title = "valid title"
			};

			_postArticleModels = ListHelper.CreateListOfType<PostArticleModel>(5);

			_getArticleModel = new GetArticleModel();
			_getArticleModels = ListHelper.CreateListOfType<GetArticleModel>(5);
		}

		[Fact]
		public async Task ThrowError_WhenModelStateError()
		{
			// Arrange
			_controller.ModelState.AddModelError("", "");

			// Assert
			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.PostArticleAsync(null));
			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.PostArticlesAsync(null));
			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.PutArticleAsync(0, null));
			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.VoteArticleAsync(0, false));
			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.DeleteArticleAsync(0));
		}

		[Fact]
		public async Task Post_PostModel_And_Return_GetModel_On_PostArticleAsync()
		{
			// Arrange
			_mockHelper.Setup(h => h.PostEntityModelAsync(It.IsAny<PostArticleModel>()))
				.ReturnsAsync(_getArticleModel);

			// Act
			IActionResult response = await _controller.PostArticleAsync(_postArticleModel);

			// Assert
			_mockHelper.Verify(h => h.PostEntityModelAsync(_postArticleModel), Times.Once);

			Assert.IsAssignableFrom<OkObjectResult>(response);
			Assert.IsType<GetArticleModel>(((OkObjectResult)response).Value);

			var modelResponse = (response as OkObjectResult).Value as GetArticleModel;
			Assert.Equal(_getArticleModel, modelResponse);
		}

		[Fact]
		public async Task Post_PostModels_On_PostArticlesAsync()
		{
			// Arrange
			_mockHelper.Setup(h => h.PostEntityModelsAsync(It.IsAny<List<PostArticleModel>>()));

			// Act
			await _controller.PostArticlesAsync(_postArticleModels);

			// Assert
			_mockHelper.Verify(h => h.PostEntityModelsAsync(_postArticleModels), Times.Once);
		}

		[Fact]
		public async Task GetArticleAsync()
		{
			// Arrange
			_mockHelper.Setup(h => h.GetEntityModelAsync(It.IsAny<int>())).ReturnsAsync(_getArticleModel);

			// Act
			var response = await _controller.GetArticleAsync(10);

			// Assert
			_mockHelper.Verify(h => h.GetEntityModelAsync(It.IsAny<int>()), Times.Once);

			Assert.IsAssignableFrom<OkObjectResult>(response);
			Assert.IsType<GetArticleModel>(((OkObjectResult)response).Value);

			var modelResponse = (response as OkObjectResult).Value as GetArticleModel;
			Assert.Equal(_getArticleModel, modelResponse);
		}

		[Fact]
		public async Task GetArticlesAsync()
		{
			// Arrange
			_mockHelper.Setup(h => h.GetAllEntityModelsAsync()).ReturnsAsync(_getArticleModels);

			// Act
			var response = await _controller.GetArticlesAsync();

			// Assert
			_mockHelper.Verify(h => h.GetAllEntityModelsAsync(), Times.Once);

			Assert.IsAssignableFrom<OkObjectResult>(response);
		}

		[Fact]
		public async Task VoteArticleAsync()
		{
			//_mockArticleVoter.Setup(v => v.VoteEntityAsync(It.IsAny<int>(), It.IsAny<bool>()));

			// Act
			await _controller.VoteArticleAsync(0, true);

			// Assert
			_mockArticleVoter.Verify(v => v.VoteEntityAsync(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
		}

		[Fact]
		public async Task SoftDeleteEntity_On_DeleteArticleAsync()
		{
			// Act
			await _controller.DeleteArticleAsync(0);

			// Assert
			_mockHelper.Verify(h => h.SoftDeleteEntityAsync(It.IsAny<int>()), Times.Once);
		}
	}
}
