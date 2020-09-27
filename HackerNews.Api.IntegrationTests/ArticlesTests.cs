using AutoMapper;
using DeepEqual.Syntax;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.Api.IntegrationTests
{

	public class ArticlesTests : IClassFixture<IntegrationTest>
	{
		private readonly HttpClient _client;
		private readonly IMapper _mapper;
		private readonly IntegrationTest _factory;

		public ArticlesTests(IntegrationTest testFixture)
		{
			_client = testFixture.Client;
			_mapper = testFixture.Mapper;
			_factory = testFixture;

			_factory.ResetDatabaseContext();
		}

		[Fact]
		public async Task PostArticle_AddsToDB_Returns_OkResult()
		{
			// Arrange
			var postModel = GetValid_PostArticleModel();
			var initialNumOfArticles = DatabaseHelper.Articles.Count();

			// Act
			var response = await _client.PostAsJsonAsync("articles", postModel);

			// Assert
			response.EnsureSuccessStatusCode();

			using (var scope = _factory.Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();
				Assert.Equal(ctx.Articles.Count(), initialNumOfArticles + 1);
			}
		}

		[Fact]
		public async Task Invalid_PostArticle_NotAddToDB_Returns_BadRequest()
		{
			// Arrange
			var postModel = GetInvalid_PostArticleModel();
			var initialNumOfArticles = DatabaseHelper.Articles.Count();

			// Act
			var response = await _client.PostAsJsonAsync("articles", postModel);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

			using (var scope = _factory.Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();
				Assert.Equal(ctx.Articles.Count(), initialNumOfArticles);
			}
		}

		[Fact]
		public async Task PostArticles_AddsToDB_Returns_OkResult()
		{
			// Arrange
			var postModel = GetValid_PostArticleModel();
			var postModelList = new List<PostArticleModel> { postModel };

			var initialNumOfArticles = DatabaseHelper.Articles.Count();
			var numOfAddedArticles = postModelList.Count();

			// Act
			var response = await _client.PostAsJsonAsync("articles/range", postModelList);

			// Assert
			response.EnsureSuccessStatusCode();

			using (var scope = _factory.Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();
				Assert.Equal(ctx.Articles.Count(), initialNumOfArticles + numOfAddedArticles);
			}
		}

		[Fact]
		public async Task Invalid_PostArticles_NotAddsToDB_Returns_BadRequest()
		{
			// Arrange
			var postModel = GetInvalid_PostArticleModel();
			var postModelList = new List<PostArticleModel> { postModel };

			var initialNumOfArticles = DatabaseHelper.Articles.Count();

			// Act
			var response = await _client.PostAsJsonAsync("articles", postModelList);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

			using (var scope = _factory.Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();
				Assert.Equal(ctx.Articles.Count(), initialNumOfArticles);
			}
		}

		[Fact]
		public async Task Valid_GetArticles_Returns_ArticleModels()
		{

			// Arrange
			var expectedModels = _mapper.Map<List<Article>, List<GetArticleModel>>(DatabaseHelper.Articles);

			// Act
			var models = await _client.GetFromJsonAsync<List<GetArticleModel>>("articles");

			// Assert
			models.OrderBy(m => m.Id).ShouldDeepEqual(expectedModels.OrderBy(m => m.Id));
		}

		[Fact]
		public async Task Valid_GetArticle_Returns_ArticleModel()
		{
			// Arrange
			var expectedModel = _mapper.Map<Article, GetArticleModel>(DatabaseHelper.Articles.FirstOrDefault());
			var articleId = 1;
			// Act
			var model = await _client.GetFromJsonAsync<GetArticleModel>($"articles({articleId})");

			// Assert
			model.ShouldDeepEqual(expectedModel);
		}

		[Fact]
		public async Task Invalid_GetArticle_Returns_Null()
		{
			// Act
			await Assert.ThrowsAnyAsync<Exception>(() =>
				_client.GetFromJsonAsync<GetArticleModel>($"articles({-1})"));
		}

		[Fact]
		public async Task Valid_PutArticle_Returns_Ok()
		{
			// Arrange
			var oldModel = _mapper.Map<Article, GetArticleModel>(DatabaseHelper.Articles.First());
			var updatedPostModel = new PostArticleModel
			{
				//AuthorName = "Updated name",
				Text = "updated text",
				Title = "valid title",
				Type = ArticleType.Opinion.ToString()
			};

			// Act
			var response = await _client.PutAsJsonAsync<PostArticleModel>($"articles({oldModel.Id})", updatedPostModel);

			// Assert
			response.EnsureSuccessStatusCode();

			// as well as in the post tests, we should ensure that the return type is correct. i can't quite figure out how to deserialize the content returned
		}

		[Fact]
		public async Task Invalid_PutArticle_Returns_BadRequest()
		{
			// Arrange
			var invalidPostModel = GetInvalid_PostArticleModel();

			// Act
			var response = await _client.PutAsJsonAsync<PostArticleModel>($"articles({1})", invalidPostModel);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Valid_VoteArticle_Returns_Ok()
		{
			// Arrange
			var originalArticle = DatabaseHelper.Articles.Find(a => a.Id == 1);
			var originalKarma = originalArticle.Karma;

			// Act
			var result = await _client.PostAsJsonAsync($"articles/vote/{originalArticle.Id}", true);

			// Assert
			result.EnsureSuccessStatusCode();

			using (var scope = _factory.Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();
				var votedArticle = ctx.Articles.Find(originalArticle.Id);

				Assert.Equal(originalKarma + 1, votedArticle.Karma);
			}
		}

		[Fact]
		public async Task Invalid_VoteArticle_Returns_BadRequest()
		{
			// Act
			var result = await _client.PostAsJsonAsync($"articles/vote/{1}", new object());

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[Fact]
		public async Task Valid_DeleteArticle_Returns_Ok()
		{
			// Arrange
			var validId = DatabaseHelper.Articles.First().Id;

			// Act
			var response = await _client.DeleteAsync($"articles({validId})");

			// Assert
			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task Invalid_DeleteArticle_Returns_NotFound()
		{
			// Act
			var response = await _client.DeleteAsync($"articles({-1})");

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		private static PostArticleModel GetValid_PostArticleModel() => new PostArticleModel
		{
			//AuthorName = "valid name",
			Text = "valid text",
			Title = "valid title",
			Type = ArticleType.Meta.ToString()
		};

		private static PostArticleModel GetInvalid_PostArticleModel() => new PostArticleModel
		{
		};
	}
}
