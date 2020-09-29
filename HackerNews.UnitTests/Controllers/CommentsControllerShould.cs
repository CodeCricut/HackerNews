//using HackerNews.Api.Controllers;
//using HackerNews.Api.Helpers;
//using HackerNews.Api.Helpers.EntityHelpers;
//using HackerNews.Domain;
//using HackerNews.Domain.Errors;
//using HackerNews.Domain.Models.Comments;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNews.UnitTests.Controllers
//{
//	public class CommentsControllerShould
//	{
//		public CommentsController _controller;

//		public Mock<IEntityService<Comment, PostCommentModel, GetCommentModel>> _mockHelper;
//		public Mock<IVoteableEntityService<Comment>> _mockVoter;

//		public PostCommentModel _postCommentModel;
//		public List<PostCommentModel> _postCommentModels;

//		public GetCommentModel _getCommentModel;
//		public List<GetCommentModel> _getCommentModels;

//		public CommentsControllerShould()
//		{
//			_mockHelper = new Mock<IEntityService<Comment, PostCommentModel, GetCommentModel>>();
//			_mockVoter = new Mock<IVoteableEntityService<Comment>>();

//			_controller = new CommentsController(_mockHelper.Object, _mockVoter.Object);

//			_postCommentModel = new PostCommentModel();
//			_postCommentModels = ListHelper.CreateListOfType<PostCommentModel>(5);

//			_getCommentModel = new GetCommentModel();
//			_getCommentModels = ListHelper.CreateListOfType<GetCommentModel>(5);
//		}

//		[Fact]
//		public async Task ThrowError_WhenModelStateError()
//		{
//			// Arrange
//			_controller.ModelState.AddModelError("", "");

//			// Assert
//			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.PostCommentAsync(null));
//			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.PostCommentsAsync(null));
//			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.PutCommentAsync(0, null));
//			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.VoteCommentAsync(0, false));
//			await Assert.ThrowsAsync<InvalidPostException>(async () => await _controller.DeleteCommentAsync(0));
//		}

//		[Fact]
//		public async Task Post_PostModel_And_Return_GetModel_On_PostCommentAsync()
//		{
//			// Arrange
//			_mockHelper.Setup(h => h.PostEntityModelAsync(It.IsAny<PostCommentModel>()))
//				.ReturnsAsync(_getCommentModel);

//			// Act
//			IActionResult response = await _controller.PostCommentAsync(_postCommentModel);

//			// Assert
//			_mockHelper.Verify(h => h.PostEntityModelAsync(_postCommentModel), Times.Once);

//			Assert.IsAssignableFrom<OkObjectResult>(response);
//			Assert.IsType<GetCommentModel>(((OkObjectResult)response).Value);

//			var modelResponse = (response as OkObjectResult).Value as GetCommentModel;
//			Assert.Equal(_getCommentModel, modelResponse);
//		}

//		[Fact]
//		public async Task Post_PostModels_On_PostCommentsAsync()
//		{
//			// Arrange
//			_mockHelper.Setup(h => h.PostEntityModelsAsync(It.IsAny<List<PostCommentModel>>()));

//			// Act
//			await _controller.PostCommentsAsync(_postCommentModels);

//			// Assert
//			_mockHelper.Verify(h => h.PostEntityModelsAsync(_postCommentModels), Times.Once);
//		}

//		[Fact]
//		public async Task GetCommentAsync()
//		{
//			// Arrange
//			_mockHelper.Setup(h => h.GetEntityModelAsync(It.IsAny<int>())).ReturnsAsync(_getCommentModel);

//			// Act
//			var response = await _controller.GetCommentAsync(10);

//			// Assert
//			_mockHelper.Verify(h => h.GetEntityModelAsync(It.IsAny<int>()), Times.Once);

//			Assert.IsAssignableFrom<OkObjectResult>(response);
//			Assert.IsType<GetCommentModel>(((OkObjectResult)response).Value);

//			var modelResponse = (response as OkObjectResult).Value as GetCommentModel;
//			Assert.Equal(_getCommentModel, modelResponse);
//		}

//		[Fact]
//		public async Task GetCommentsAsync()
//		{
//			// Arrange
//			_mockHelper.Setup(h => h.GetAllEntityModelsAsync()).ReturnsAsync(_getCommentModels);

//			// Act
//			var response = await _controller.GetCommentsAsync();

//			// Assert
//			_mockHelper.Verify(h => h.GetAllEntityModelsAsync(), Times.Once);

//			Assert.IsAssignableFrom<OkObjectResult>(response);
//		}

//		[Fact]
//		public async Task VoteArticleAsync()
//		{
//			// Act
//			await _controller.VoteCommentAsync(0, true);

//			// Assert
//			_mockVoter.Verify(v => v.VoteEntityAsync(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
//		}

//		[Fact]
//		public async Task SoftDeleteEntity_On_DeleteArticleAsync()
//		{
//			// Act
//			await _controller.DeleteCommentAsync(0);

//			// Assert
//			_mockHelper.Verify(h => h.SoftDeleteEntityAsync(It.IsAny<int>()), Times.Once);
//		}
//	}
//}
