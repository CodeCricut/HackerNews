//using HackerNews.Domain;
//using HackerNews.Domain.Errors;
//using HackerNews.Domain.Models;
//using Moq;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNews.UnitTests.Helpers
//{
//	public class ArticleHelperShould : EntityHelperShould
//	{

//		#region EntityHelperTests
//		[Fact]
//		public async Task Add_Converted_PostModel_To_Repo_And_Save_And_Return_GetModel()
//		{
//			// Arrange
//			_mockArticleConverter.Setup(c => c.ConvertEntityModelAsync(It.IsAny<PostArticleModel>()))
//				.ReturnsAsync(_article);
//			_mockArticleConverter.Setup(c => c.ConvertEntityAsync<GetArticleModel>(It.IsAny<Article>()))
//				.ReturnsAsync(_getArticleModel);

//			// Act
//			var response = await _articleHelper.PostEntityModelAsync(_postArticleModel);

//			// Assert
//			_mockArticleConverter.Verify(c => c.ConvertEntityModelAsync(It.IsAny<PostArticleModel>()), Times.Once);

//			_mockArticleRepository.Verify(c => c.AddEntityAsync(_article), Times.Once);
//			_mockArticleRepository.Verify(c => c.SaveChangesAsync(), Times.Once);

//			_mockArticleConverter.Verify(c => c.ConvertEntityAsync<GetArticleModel>(It.IsAny<Article>()), Times.Once);

//			Assert.IsType<GetArticleModel>(response);
//		}

//		[Fact]
//		public async Task Add_Converted_PostModels_To_To_Repo_And_Save()
//		{
//			// Act
//			await _articleHelper.PostEntityModelsAsync(_postArticleModels);

//			// Assert
//			_mockArticleConverter.Verify(c => c.ConvertEntityModelsAsync(It.IsAny<List<PostArticleModel>>()), Times.Once);

//			_mockArticleRepository.Verify(r => r.AddEntititesAsync(It.IsAny<List<Article>>()), Times.Once);
//			_mockArticleRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
//		}

//		[Fact]
//		public async Task Get_And_Update_And_Save_Converted_EntityModel_On_PutEntityModel()
//		{
//			_mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).ReturnsAsync(_article);
//			_mockArticleConverter.Setup(c => c.ConvertEntityModelAsync(It.IsAny<PostArticleModel>())).ReturnsAsync(_article);
//			_mockArticleConverter.Setup(c => c.ConvertEntityAsync<GetArticleModel>(It.IsAny<Article>())).ReturnsAsync(_getArticleModel);

//			// Act
//			var response = await _articleHelper.PutEntityModelAsync(0, _postArticleModel);

//			// Assert
//			_mockArticleRepository.Verify(r => r.GetEntityAsync(It.IsAny<int>()), Times.AtLeastOnce);
//			_mockArticleRepository.Verify(r => r.UpdateEntityAsync(It.IsAny<int>(), It.IsAny<Article>()), Times.Once);

//			_mockArticleConverter.Verify(c => c.ConvertEntityModelAsync(It.IsAny<PostArticleModel>()), Times.Once);

//			_mockArticleRepository.Verify(r => r.UpdateEntityAsync(It.IsAny<int>(), It.IsAny<Article>()), Times.Once);

//			Assert.IsType<GetArticleModel>(response);
//		}

//		[Fact]
//		public async Task SoftDeleteEntity_And_Save()
//		{
//			_mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).ReturnsAsync(_article);

//			// Act
//			await _articleHelper.SoftDeleteEntityAsync(0);

//			// Assert
//			_mockArticleRepository.Verify(r => r.SoftDeleteEntityAsync(It.IsAny<int>()), Times.Once);
//			_mockArticleRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
//		}

//		[Fact]
//		public async Task ReturnEntity_WhenIdValid_On_GetEntityAsync()
//		{
//			_mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>()))
//				.ReturnsAsync(_article);

//			// Act
//			var result = await _articleHelper.GetEntityAsync(0);

//			// Assert
//			_mockArticleRepository.Verify(r => r.GetEntityAsync(It.IsAny<int>()), Times.Once);

//			Assert.IsType<Article>(result);
//		}

//		[Fact]
//		public async Task Throw_NotFoundException_WhenEntityNull_On_GetEntityAsync()
//		{
//			// _mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).Returns(null);

//			await Assert.ThrowsAnyAsync<NotFoundException>(() => _articleHelper.GetEntityAsync(0));
//		}

//		[Fact]
//		public async Task Get_And_Trim_Entities_On_GetAllEntiteisAsync()
//		{
//			_mockArticleRepository.Setup(r => r.GetEntitiesAsync()).ReturnsAsync(_articles);

//			// Act
//			var result = await _articleHelper.GetAllEntitiesAsync();

//			// Assert
//			_mockArticleRepository.Verify(r => r.GetEntitiesAsync(), Times.Once);

//			Assert.IsType<List<Article>>(result);
//		}

//		[Fact]
//		public async Task GetAllEntityModelsAsync()
//		{
//			_mockArticleRepository.Setup(r => r.GetEntitiesAsync()).ReturnsAsync(_articles);
//			_mockArticleConverter.Setup(c => c.ConvertEntitiesAsync<GetArticleModel>(It.IsAny<List<Article>>())).ReturnsAsync(_getArticleModels);

//			// Act
//			var result = await _articleHelper.GetAllEntityModelsAsync();

//			// Assert
//			Assert.IsType<List<GetArticleModel>>(result);

//			_mockArticleRepository.Verify(r => r.GetEntitiesAsync(), Times.Once);
//			_mockArticleConverter.Verify(c => c.ConvertEntitiesAsync<GetArticleModel>(It.IsAny<List<Article>>()), Times.Once);

//		}
//		#endregion

//		#region ArticleHelperTests
//		[Fact]
//		public async Task Get_And_Vote_On_VoteEntityAsync()
//		{
//			// Arrange
//			_mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).ReturnsAsync(_article);
//			var originalKarma = _article.Karma;

//			// Act
//			await _articleHelper.VoteEntityAsync(0, true);

//			// Assert
//			_mockArticleRepository.Verify(r => r.GetEntityAsync(It.IsAny<int>()), Times.AtLeastOnce);
//			_mockArticleRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

//			Assert.Equal(originalKarma + 1, _article.Karma);
//		}

//		[Fact]
//		public async Task Get_And_Convert_To_GetModel_And_Return_Model_On_GetEntitModelAsync()
//		{
//			// Arrange
//			_mockArticleRepository.Setup(r => r.GetEntityAsync(It.IsAny<int>())).ReturnsAsync(_article);
//			_mockArticleConverter.Setup(c => c.ConvertEntityAsync<GetArticleModel>(It.IsAny<Article>())).ReturnsAsync(_getArticleModel);

//			// Act
//			var result = await _articleHelper.GetEntityModelAsync(0);

//			// Assert
//			_mockArticleRepository.Verify(r => r.GetEntityAsync(It.IsAny<int>()), Times.Once);
//			_mockArticleConverter.Verify(c => c.ConvertEntityAsync<GetArticleModel>(It.IsAny<Article>()), Times.Once);

//			Assert.IsType<GetArticleModel>(result);
//		}

//		[Fact]
//		public async Task Return_AllEntities_On_GetAllEntitiesAsync()
//		{
//			// Arrange
//			_mockArticleRepository.Setup(r => r.GetEntitiesAsync()).ReturnsAsync(_articles);

//			// Act
//			var result = await _articleHelper.GetAllEntitiesAsync();

//			// Assert
//			_mockArticleRepository.Verify(r => r.GetEntitiesAsync(), Times.Once);

//			Assert.IsType<List<Article>>(result);

//		}
//		#endregion
//	}
//}
