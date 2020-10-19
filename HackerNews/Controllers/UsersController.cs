using CleanEntityArchitecture.Domain;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using HackerNews.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class UsersController : Controller
	{
		private static readonly string USER_ENDPOINT = "users";
		private readonly IJwtService _jwtService;
		private readonly IApiModifier<User, RegisterUserModel, GetPrivateUserModel> _privateUserModifier;
		private readonly IApiLoginFacilitator<LoginModel, GetPrivateUserModel> _loginFacilitator;
		private readonly IApiReader _apiReader;

		public UsersController(
			IJwtService jwtService,
			IApiModifier<User, RegisterUserModel, GetPrivateUserModel> privateUserModifier,
			IApiLoginFacilitator<LoginModel, GetPrivateUserModel> loginFacilitator,
			IApiReader apiReader
			)
		{
			_jwtService = jwtService;
			_privateUserModifier = privateUserModifier;
			_loginFacilitator = loginFacilitator;
			_apiReader = apiReader;
		}

		public IActionResult Index()
		{
			return View();
		}


		public ViewResult Register()
		{
			var model = new UserRegisterViewModel { PostModel = new RegisterUserModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUserModel registerModel)
		{
			GetPrivateUserModel addedUser = await _privateUserModifier.PostEndpointAsync($"{USER_ENDPOINT}/register", registerModel);
			return RedirectToAction("Login");
		}


		public ViewResult Login()
		{
			var model = new UserLoginViewModel { PostModel = new LoginModel() };
			return View(model);
		}

		public ActionResult Logout()
		{
			_jwtService.RemoveToken();
			return RedirectToAction("Register");
		}


		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel viewModel)
		{
			var loginModel = viewModel.PostModel;
			await _loginFacilitator.LogIn(loginModel);

			return RedirectToAction("Me");
		}

		public async Task<ViewResult> Me()
		{
			GetPrivateUserModel privateModel = await _apiReader.GetEndpointAsync<GetPrivateUserModel>($"{USER_ENDPOINT}/me");
			var model = new PrivateUserDetailsViewModel { GetModel = privateModel };

			return View(model);
		}

		public async Task<ActionResult<PublicUserDetailsViewModel>> Details(int id)
		{
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, id);

			var returnModel = new PublicUserDetailsViewModel { GetModel = user };
			return View(returnModel);
		}

		public async Task<ActionResult<UsersListViewModel>> List(int pageNumber, int pageSize)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var users = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, pagingParams);

			var returnModel = new UsersListViewModel { GetModels = users.Items };
			return View(returnModel);
		}

		public async Task<ActionResult<UserArticlesListView>> Articles(int userId)
		{
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, userId);

			// TODO: refactor to service and null checks and all that jazz
			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", user.ArticleIds);
			var model = new UserArticlesListView { GetModels = articles };
			return View(model);
		}

		public async Task<ActionResult<UserCommentsListView>> Comments(int userId)
		{
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>(USER_ENDPOINT, userId);

			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", user.CommentIds);
			var model = new UserCommentsListView { GetModels = comments };
			return View(model);
		}

		public async Task<ActionResult<UserSavedView>> Saved()
		{
			GetPrivateUserModel privateModel = await _apiReader.GetEndpointAsync<GetPrivateUserModel>($"{USER_ENDPOINT}/me", 0);

			var articles = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", privateModel.ArticleIds);
			var comments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", privateModel.CommentIds);
			var model = new UserSavedView { SavedArticles = articles, SavedComments = comments };
			return View(model);
		}

		public async Task<ActionResult<UserBoardsView>> Boards()
		{
			GetPrivateUserModel privateModel = await _apiReader.GetEndpointAsync<GetPrivateUserModel>($"{USER_ENDPOINT}/me", 0);

			var boardsSubscribed = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", privateModel.BoardsSubscribed);
			var boardsModerating = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", privateModel.BoardsModerating);
			var model = new UserBoardsView { BoardsModerating = boardsModerating, BoardsSubscribed = boardsSubscribed };
			return View(model);
		}
	}
}
