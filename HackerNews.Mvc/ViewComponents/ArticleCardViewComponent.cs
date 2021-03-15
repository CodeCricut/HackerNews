using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Common.Helpers;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Services;
using HackerNews.Mvc.Services.Interfaces;
using HackerNews.Mvc.ViewModels.ViewComponents.ArticleCard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
{
	public class ArticleCardViewComponent : ViewComponent
	{
		private readonly IMediator _mediator;
		private readonly IApiSignInManager _apiJwtManager;

		public ArticleCardViewComponent(IMediator mediator, IApiSignInManager apiAccountClient)
		{
			_mediator = mediator;
			_apiJwtManager = apiAccountClient;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetArticleModel articleModel, string imageDataUrl, bool displayText = false)
		{
			// TODO: test with deleted boards
			var getBoardQuery = new GetBoardQuery(articleModel.BoardId);
			GetBoardModel board = await getBoardQuery.DefaultIfExceptionAsync(_mediator);

			// TODO: returns null, which throws exception in view, if user is deleted
			var getuserQuery = new GetPublicUserQuery(articleModel.UserId);
			GetPublicUserModel user = await getuserQuery.DefaultIfExceptionAsync(_mediator);
			
			string jwt = _apiJwtManager.GetToken();

			bool loggedIn = false;
			bool saved = false;
			bool userUpvoted = false;
			bool userDownvoted = false;
			bool userCreatedArticle = false;
			try
			{
				var loggedInUser = await _mediator.Send(new GetAuthenticatedUserQuery());
				if (loggedInUser != null) loggedIn = true;
				saved = loggedInUser.SavedArticles.Any(articleId => articleId == articleModel.Id);
				userUpvoted = loggedInUser.LikedArticles.Any(articleId => articleId == articleModel.Id);
				userDownvoted = loggedInUser.DislikedArticles.Any(articleId => articleId == articleModel.Id);
				userCreatedArticle = loggedInUser.ArticleIds.Any(articleId => articleId == articleModel.Id);
			}
			catch { }

			var viewModel = new ArticleCardViewModel
			{
				Article = articleModel,
				Board = board,
				User = user,
				Jwt = jwt,
				LoggedIn = loggedIn,
				Saved = saved,
				UserUpvoted = userUpvoted,
				UserDownvoted = userDownvoted,
				UserCreatedArticle = userCreatedArticle,
				ImageDataUrl = imageDataUrl,
				DisplayText = displayText
			};
			return View(viewModel);
		}
	}
}
