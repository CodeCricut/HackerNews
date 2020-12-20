using AutoMapper;
using HackerNews.Api;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Common
{
	/// <summary>
	/// A base integration test.
	/// </summary>
	public abstract class AppIntegrationTest
		: IDisposable
	{
		/// <summary>
		/// Bootstraps the app into memory through <see cref="HackerNews.Api.Startup"/>. <seealso cref="CustomWebApplicationFactory{TStartup}"/> 
		/// </summary>
		public CustomWebApplicationFactory<Startup> Factory { get; }
		public HttpClient Client { get; private set; }

		//===== Commonly used properties =====//
		protected IMediator mediator;
		protected IMapper mapper;
		protected UserManager<User> userManager;
		protected IUnitOfWork unitOfWork;

		protected IQueryable<User> users;
		protected User user;
		protected Mock<IDeletedEntityPolicyValidator<User>> deletedUserValidatorMock;

		/// <summary>
		/// <code>UserId</code> property returns <see cref="user"/> Id by default. Can be configured with 
		/// <code>.Setup(mock => mock.UserId).Returns(value)</code>
		/// </summary>
		protected Mock<ICurrentUserService> currentUserServiceMock;

		protected IQueryable<Board> boards;
		protected Board board;
		protected Mock<IDeletedEntityPolicyValidator<Board>> deletedBoardValidatorMock;

		protected IQueryable<Article> articles;
		protected Article article;
		protected Mock<IDeletedEntityPolicyValidator<Article>> deletedArticleValidatorMock;

		protected IQueryable<Comment> comments;
		protected Comment comment;
		protected Mock<IDeletedEntityPolicyValidator<Comment>> deletedCommentValidatorMock;

		public AppIntegrationTest()
		{
			Factory = new CustomWebApplicationFactory<Startup>();
			Client = Factory.CreateClient();

			InstantiateCommonProperties().GetAwaiter().GetResult();
		}

		private async Task InstantiateCommonProperties()
		{
			var scope = Factory.Services.CreateScope();

			//===== Other =====//
			mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
			userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			//===== Users =====//
			users = await unitOfWork.Users.GetEntitiesAsync();
			user = users.First();
			deletedUserValidatorMock = new Mock<IDeletedEntityPolicyValidator<User>>();
			deletedUserValidatorMock.Setup(m => m.ValidateEntityQuerable(It.IsAny<IQueryable<User>>(), It.IsAny<DeletedEntityPolicy>())).Returns(users);
			deletedUserValidatorMock.Setup(m => m.ValidateEntity(It.IsAny<User>(), It.IsAny<DeletedEntityPolicy>())).Returns(user);

			currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			//===== Boards =====//
			boards = await unitOfWork.Boards.GetEntitiesAsync();
			board = boards.First();
			deletedBoardValidatorMock = new Mock<IDeletedEntityPolicyValidator<Board>>();
			deletedBoardValidatorMock.Setup(m => m.ValidateEntityQuerable(It.IsAny<IQueryable<Board>>(), It.IsAny<DeletedEntityPolicy>())).Returns(boards);
			deletedBoardValidatorMock.Setup(m => m.ValidateEntity(It.IsAny<Board>(), It.IsAny<DeletedEntityPolicy>())).Returns(board);

			//===== Articles =====//
			articles = await unitOfWork.Articles.GetEntitiesAsync();
			article = articles.First();
			deletedArticleValidatorMock = new Mock<IDeletedEntityPolicyValidator<Article>>();
			deletedArticleValidatorMock.Setup(m => m.ValidateEntityQuerable(It.IsAny<IQueryable<Article>>(), It.IsAny<DeletedEntityPolicy>())).Returns(articles);
			deletedArticleValidatorMock.Setup(m => m.ValidateEntity(It.IsAny<Article>(), It.IsAny<DeletedEntityPolicy>())).Returns(article);

			//===== Comments =====//
			comments = await unitOfWork.Comments.GetEntitiesAsync();
			comment = comments.First();
			deletedCommentValidatorMock = new Mock<IDeletedEntityPolicyValidator<Comment>>();
			deletedCommentValidatorMock.Setup(m => m.ValidateEntityQuerable(It.IsAny<IQueryable<Comment>>(), It.IsAny<DeletedEntityPolicy>())).Returns(comments);
			deletedCommentValidatorMock.Setup(m => m.ValidateEntity(It.IsAny<Comment>(), It.IsAny<DeletedEntityPolicy>())).Returns(comment);




		}

		public void Dispose()
		{
			using var scope = Factory.Services.CreateScope();
			var context = scope.ServiceProvider.GetService<HackerNewsContext>();
			var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
			context.ClearDatabase();
			context.InitializeForTestsAsync(userManager).GetAwaiter().GetResult();
		}
	}
}
