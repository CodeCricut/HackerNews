//using HackerNews.Domain;
//using HackerNews.Domain.Models;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNews.UnitTests.Converters
//{
//	public class CommentConverterShould : EntityConverterShould
//	{

//		public CommentConverterShould()
//		{
//		}


//		[Fact]
//		public async Task ConvertEntityAsync_Returns_GetModel()
//		{
//			// Act
//			var getModel = await _commentConverter.ConvertEntityAsync<GetCommentModel>(_comment);

//			// Assert
//			Assert.IsType<GetCommentModel>(getModel);

//			_mockMapper.Verify(m => m.Map<GetCommentModel>(It.IsAny<Comment>()), Times.Once);
//		}

//		[Fact]
//		public async Task ConvertEntitiesAsync_Returns_GetModels()
//		{
//			// Arrange
//			int numOfComments = _comments.Count;

//			// Act
//			var getModels = await _commentConverter.ConvertEntitiesAsync<GetCommentModel>(_comments);

//			// Assert
//			Assert.IsType<List<GetCommentModel>>(getModels);

//			_mockMapper.Verify(m => m.Map<GetCommentModel>(It.IsAny<Comment>()), Times.Exactly(numOfComments));
//		}

//		[Fact]
//		public async Task ConvertPostModelAsync_Returns_Entity()
//		{
//			// Arrange
//			const int parentArticleId = 1;
//			const int parentCommentId = 2;

//			_parentArticle.Id = parentArticleId;
//			_parentComment.Id = parentCommentId;

//			_postCommentModel.ParentArticleId = parentArticleId;
//			_postCommentModel.ParentCommentId = parentCommentId;


//			_mockCommentRepo.Setup(cr => cr.GetEntityAsync(parentCommentId))
//				.ReturnsAsync(_parentComment);
//			_mockArticleRepo.Setup(cr => cr.GetEntityAsync(parentArticleId))
//				.ReturnsAsync(_parentArticle);

//			// Act
//			var comment = await _commentConverter.ConvertEntityModelAsync(_postCommentModel);

//			// Assert
//			Assert.IsType<Comment>(comment);

//			_mockMapper.Verify(m => m.Map<Comment>(It.IsAny<PostCommentModel>()), Times.Once);

//			_mockCommentRepo.Verify(cr => cr.GetEntityAsync(It.IsAny<int>()), Times.Once);
//			_mockArticleRepo.Verify(ar => ar.GetEntityAsync(It.IsAny<int>()), Times.Once);

//			Assert.Equal(_parentArticle, comment.ParentArticle);
//			Assert.Equal(_parentComment, comment.ParentComment);
//		}
//	}
//}
