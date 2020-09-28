using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.UnitTests.Helpers
{
	public class ArticleHelperShould : EntityHelperShould
	{

		#region EntityHelperTests
		[Fact]
		public async Task Add_Converted_PostModel_To_Repo_And_Save_And_Return_GetModel()
		{
			// Arrange

			// Act
			var response = await _articleHelper.PostEntityModelAsync(_postArticleModel);

			// Assert
			_mockMapper.Verify(m => m.Map<Article>(_postArticleModel), Times.Once);

			_mockArticleRepository.Verify(c => c.AddEntityAsync(_article), Times.Once);
			_mockArticleRepository.Verify(c => c.SaveChangesAsync(), Times.Once);

			_mockMapper.Verify(m => m.Map<GetArticleModel>(It.IsAny<Article>()), Times.Once);

			Assert.IsType<GetArticleModel>(response);
			Assert.Equal(_getArticleModel, response);
		}

		[Fact]
		public async Task Add_Converted_PostModels_To_To_Repo_And_Save()
		{
			// Act
			await _articleHelper.PostEntityModelsAsync(_postArticleModels);

			// Assert

			_mockArticleRepository.Verify(r => r.AddEntititesAsync(It.IsAny<List<Article>>()), Times.Once);
			_mockArticleRepository.Verify(r => r.SaveChangesAsync(), Times.Once);


			_mockMapper.Verify(m => m.Map<List<Article>>(_postArticleModels), Times.Once);

			_mockArticleRepository.Verify(c => c.AddEntititesAsync(_articles), Times.Once);
			_mockArticleRepository.Verify(c => c.SaveChangesAsync(), Times.Once);
		}

		[Fact]
		public async Task SoftDeleteEntity_And_Save()
		{
			// Arrange

			// Act
			await _articleHelper.SoftDeleteEntityAsync(0);

			// Assert
			_mockArticleRepository.Verify(r => r.SoftDeleteEntityAsync(It.IsAny<int>()), Times.Once);
			_mockArticleRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
		}



		[Fact]
		public async Task GetAllEntityModelsAsync()
		{
			// Arrange

			// Act
			var result = await _articleHelper.GetAllEntityModelsAsync();

			// Assert
			Assert.IsType<List<GetArticleModel>>(result);

			_mockMapper.Verify(m => m.Map<List<GetArticleModel>>(It.IsAny<List<Article>>()), Times.Once);

			_mockArticleRepository.Verify(r => r.GetEntitiesAsync(), Times.Once);
		}
		#endregion

		#region ArticleHelperTests
		[Fact]
		public async Task Get_And_Vote_On_VoteEntityAsync()
		{
			// Arrange
			var originalKarma = _article.Karma;

			// Act
			await _articleHelper.VoteEntityAsync(0, true);

			// Assert
			_mockArticleRepository.Verify(r => r.GetEntityAsync(It.IsAny<int>()), Times.AtLeastOnce);
			_mockArticleRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

			Assert.Equal(originalKarma + 1, _article.Karma);
		}

		[Fact]
		public async Task Get_And_Convert_To_GetModel_And_Return_Model_On_GetEntitModelAsync()
		{
			// Arrange

			// Act
			var result = await _articleHelper.GetEntityModelAsync(0);

			// Assert
			_mockArticleRepository.Verify(r => r.GetEntityAsync(It.IsAny<int>()), Times.Once);

			Assert.IsType<GetArticleModel>(result);
		}
		#endregion
	}
}
