using HackerNews.Domain.Models;
using HackerNews.EF;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.Api.IntegrationTests
{
	public class CommentsTests : IClassFixture<IntegrationTest>
	{
		private readonly IntegrationTest _factory;
		private readonly HttpClient _client;

		public CommentsTests(IntegrationTest factory)
		{
			_factory = factory;
			_client = _factory.Client;

			_factory.ResetDatabaseContext();
		}

		[Fact]
		public async Task PostComment_WithoutParents_Returns_Ok()
		{
			// Arrange
			var postModel = Get_PostCommentModel_WithoutParents();

			// Act
			var result = await _client.PostAsJsonAsync("comments", postModel);

			// Assert
			result.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task PostComment_WithParents_AddsRelationships()
		{
			// Arrange
			var postModel = Get_PostCommentModel_WithParents();

			// Act
			var result = await _client.PostAsJsonAsync("comments", postModel);

			var resultJson = await result.Content.ReadAsStringAsync();
			var returnedModel = JsonConvert.DeserializeObject<GetCommentModel>(resultJson);

			// Assert
			result.EnsureSuccessStatusCode();

			Assert.NotNull(returnedModel);

			using (var scope = _factory.Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();
			
				// Assert parents were added to comment child
				var postedComment = ctx.Comments.Find(returnedModel.Id);

				Assert.Equal(postedComment.ParentArticleId, postModel.ParentArticleId);
				Assert.Equal(postedComment.ParentCommentId, postModel.ParentCommentId);

				// Assert child was added to parents
				var parentArticle = ctx.Articles.Find(returnedModel.ParentArticleId);
				Assert.Contains(postedComment, parentArticle.Comments);

				var parentComment = ctx.Comments.Find(returnedModel.ParentCommentId);
				Assert.Contains(postedComment, parentComment.ChildComments);

			}
		}



		private PostCommentModel Get_PostCommentModel_WithoutParents() => new PostCommentModel
		{
			AuthorName = "valid author name",
			Text = "valid text"
		};

		private PostCommentModel Get_PostCommentModel_WithParents() => new PostCommentModel
		{
			AuthorName = "valid author name",
			Text = "valid text",
			ParentArticleId = DatabaseHelper.Articles.FirstOrDefault().Id,
			ParentCommentId = DatabaseHelper.Comments.FirstOrDefault().Id
		};

	}
}
