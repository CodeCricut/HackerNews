//using AutoMapper;
//using HackerNews.Api.Converters;
//using HackerNews.Domain;
//using HackerNews.Domain.Models;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNews.UnitTests.Converters
//{
//	public class ArticleConverterShould : EntityConverterShould
//	{
//		public ArticleConverterShould()
//		{

//		}

//		[Fact]
//		public async Task ConvertEntityAsync_Returns_GetModel()
//		{
//			// Act
//			var getModel = await _articleConverter.ConvertEntityAsync<GetArticleModel>(_article);

//			// Assert
//			Assert.IsType<GetArticleModel>(getModel);

//			_mockMapper.Verify(m => m.Map<GetArticleModel>(It.IsAny<Article>()), Times.Once);
//		}

//		[Fact]
//		public async Task ConvertEntitiesAsync_Returns_GetModels()
//		{
//			// Arrange
//			var numOfArticles = _articles.Count;

//			// Act
//			var getModels = await _articleConverter.ConvertEntitiesAsync<GetArticleModel>(_articles);

//			// Assert
//			Assert.IsType<List<GetArticleModel>>(getModels);
//			Assert.Equal(numOfArticles, getModels.Count);

//			_mockMapper.Verify(m => m.Map<GetArticleModel>(It.IsAny<Article>()), Times.Exactly(numOfArticles));
//		}

//		[Fact]
//		public async Task ConvertPostModelAsync_Returns_Entity()
//		{
//			// Act
//			var entity = await _articleConverter.ConvertEntityModelAsync(_postArticleModel);

//			// Assert
//			Assert.IsType<Article>(entity);

//			_mockMapper.Verify(m => m.Map<Article>(It.IsAny<PostArticleModel>()), Times.Once);

//		}

//		[Fact]
//		public async Task ConvertPostModelsAsync_Returns_Entities()
//		{
//			// Arrange
//			var numOfModels = _postArticleModels.Count;

//			// Act
//			var entities = await _articleConverter.ConvertEntityModelsAsync(_postArticleModels);

//			// Assert
//			Assert.IsType<List<Article>>(entities);
//			Assert.Equal(numOfModels, entities.Count);

//			_mockMapper.Verify(m => m.Map<Article>(It.IsAny<PostArticleModel>()), Times.Exactly(numOfModels));
//		}
//	}
//}
