using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class UsersController : Controller
	{
		private static readonly string USER_ENDPOINT = "users";
		private readonly IApiModifier<User, RegisterUserModel, GetPrivateUserModel> _privateUserModifier;
		private readonly IApiLoginFacilitator<LoginModel, GetPrivateUserModel> _loginFacilitator;
		private readonly IApiReader _apiReader;

		public UsersController(
			IApiModifier<User, RegisterUserModel, GetPrivateUserModel> privateUserModifier,
			IApiLoginFacilitator<LoginModel, GetPrivateUserModel> loginFacilitator,
			IApiReader apiReader
			)
		{
			_privateUserModifier = privateUserModifier;
			_loginFacilitator = loginFacilitator;
			_apiReader = apiReader;
		}

		public ViewResult Register()
		{
			var model = new UserRegisterViewModel { RegisterModel = new RegisterUserModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
		{
			// Register
			var privateUser = await _privateUserModifier.PostEndpointAsync($"{USER_ENDPOINT}/register", viewModel.RegisterModel);

			// Login
			var loginModel = new LoginModel { Username = privateUser.Username, Password = privateUser.Password };
			await _loginFacilitator.LogIn(loginModel);
			return RedirectToAction("Me");
		}

		public ViewResult Login()
		{
			var model = new UserLoginViewModel { LoginModel = new LoginModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel viewModel)
		{
			await _loginFacilitator.LogIn(viewModel.LoginModel);

			return RedirectToAction("Me");
		}


		public async Task<IActionResult> Logout()
		{
			await _loginFacilitator.LogOut();
			return RedirectToAction("Register");
		}

		[Authorize]
		public async Task<ViewResult> Me()
		{
			GetPrivateUserModel privateModel = await _apiReader.GetEndpointAsync<GetPrivateUserModel>($"{USER_ENDPOINT}/me", includeDeleted: true);

			var pagingParams = new PagingParams { PageNumber = 1, PageSize = 10 };
			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", privateModel.ArticleIds, pagingParams, includeDeleted: true);
			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", privateModel.CommentIds, pagingParams, includeDeleted: true);

			var model = new PrivateUserDetailsViewModel
			{
				User = privateModel,
				ArticlePage = new Page<GetArticleModel>(articles),
				CommentPage = new Page<GetCommentModel>(comments)
			};

			return View(model);
		}

		public async Task<ActionResult<PublicUserDetailsViewModel>> Details(int id)
		{
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, id);

			var pagingParams = new PagingParams { PageNumber = 1, PageSize = 10 };
			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", user.ArticleIds, pagingParams);
			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", user.CommentIds, pagingParams);

			var returnModel = new PublicUserDetailsViewModel
			{
				User = user,
				ArticlePage = new Page<GetArticleModel>(articles),
				CommentPage = new Page<GetCommentModel>(comments)
			};
			return View(returnModel);
		}


		public async Task<ActionResult<UserArticlesListView>> Articles(int userId, PagingParams pagingParams)
		{
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, userId);
			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", user.ArticleIds, pagingParams);

			var model = new UserArticlesListView { ArticlePage = new Page<GetArticleModel>(articles) };
			return View(model);
		}

		public async Task<ActionResult<UserCommentsListView>> Comments(int userId, PagingParams pagingParams)
		{
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, userId);
			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", user.CommentIds, pagingParams);

			var model = new UserCommentsListView { CommentPage = new Page<GetCommentModel>(comments) };
			return View(model);
		}

		[Authorize]
		public async Task<ActionResult<UserSavedView>> Saved(PagingParams pagingParams)
		{
			GetPrivateUserModel privateModel = await _apiReader.GetEndpointAsync<GetPrivateUserModel>($"{USER_ENDPOINT}/me");

			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", privateModel.SavedArticles, pagingParams);
			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", privateModel.SavedComments, pagingParams);

			var model = new UserSavedView
			{
				SavedArticlesPage = new Page<GetArticleModel>(articles),
				SavedCommentsPage = new Page<GetCommentModel>(comments)
			};
			return View(model);
		}

		[Authorize]
		public async Task<ActionResult<UserBoardsView>> Boards(PagingParams pagingParams)
		{
			GetPrivateUserModel privateModel = await _apiReader.GetEndpointAsync<GetPrivateUserModel>($"{USER_ENDPOINT}/me", includeDeleted: true);

			var boardsSubscribed = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", privateModel.BoardsSubscribed, pagingParams);
			var boardsModerating = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", privateModel.BoardsModerating, pagingParams);
			var model = new UserBoardsView
			{
				BoardsSubscribedPage = new Page<GetBoardModel>(boardsSubscribed),
				BoardsModeratingPage = new Page<GetBoardModel>(boardsModerating)
			};
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			await _privateUserModifier.DeleteEndpointAsync(USER_ENDPOINT, id);

			return RedirectToAction("users/me");
		}
	}
}
