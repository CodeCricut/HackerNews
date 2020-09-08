﻿using AutoMapper;
using HackerNews.Api.Converters;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.UnitTests
{
	public class ArticleConverterShould
	{
		[Fact]
		public async Task ConvertEntityAsync_Returns_GetModel()
		{
			var article = new Article();

			var mockMapper = new Mock<IMapper>();

			var converter = new ArticleConverter(mockMapper.Object);
			var getModel = await converter.ConvertEntityAsync<GetArticleModel>(article);

			Assert.IsType<GetArticleModel>(getModel);
		}

		[Fact]
		public async Task ConvertEntitiesAsync_Returns_GetModels()
		{
			var articles = new List<Article>();
			for(int i = 0; i <3; i++)
			{
				articles.Add(new Article());
			}

			var mockMapper = new Mock<IMapper>();

			var converter = new ArticleConverter(mockMapper.Object);
			var getModels = await converter.ConvertEntitiesAsync<GetArticleModel>(articles);

			Assert.IsType<List<GetArticleModel>>(getModels);
		}

		[Fact]
		public async Task ConvertPostModelAsync_Returns_Entity()
		{
			var postModel = new PostArticleModel();

			var mockMapper = new Mock<IMapper>();

			var converter = new ArticleConverter(mockMapper.Object);
			var entity = await converter.ConvertEntityModelAsync(postModel);

			Assert.IsType<Article>(entity);
		}

		[Fact]
		public async Task ConvertPostModelsAsync_Returns_Entities()
		{
			var postModels = new List<PostArticleModel>();
			for (int i = 0; i < 3; i++)
			{
				postModels.Add(new PostArticleModel());
			}

			var mockMapper = new Mock<IMapper>();

			var converter = new ArticleConverter(mockMapper.Object);
			var entities = await converter.ConvertEntityModelsAsync(postModels);

			Assert.IsType<List<Article>>(entities);
		}
	}
}
