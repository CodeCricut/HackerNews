using HackerNews.Api.Controllers;
using HackerNews.Domain;
using HackerNews.EF;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.UnitTests.Controllers
{
	public class DeveloperControllerShould
	{
		[Fact]
		public void ThrowError_For_ThrowError()
		{
			var controller = new DeveloperController(null, null, null);
			Assert.ThrowsAny<Exception>(() => controller.ThrowError());
		}

		[Fact]
		public async Task FetchAndRemoveAllEntities_On_DeleteAllDataAsync()
		{
			var mockContext = new Mock<HackerNewsContext>(new DbContextOptions<HackerNewsContext>());
			var mockArticleRepo = new Mock<EntityRepository<Article>>(null);
			var mockCommentRepo = new Mock<EntityRepository<Comment>>(null);

			var allArticles = new List<Article> { new Article() };
			var allComments = new List<Comment> { new Comment() };

			mockArticleRepo.Setup(ar => ar.GetEntitiesAsync()).ReturnsAsync(allArticles);
			mockCommentRepo.Setup(cr => cr.GetEntitiesAsync()).ReturnsAsync(allComments);

			var controller = new DeveloperController(mockArticleRepo.Object, mockCommentRepo.Object, mockContext.Object);
			await controller.DeleteAllDataAsync();

			mockContext.Verify(c => c.RemoveRange(allArticles), Times.Once);
			mockContext.Verify(c => c.RemoveRange(allComments), Times.Once);

			mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
