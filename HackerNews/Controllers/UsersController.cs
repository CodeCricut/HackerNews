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
		private readonly IApiReader<GetPublicUserModel> _publicUserReader;
		private readonly IApiReader<GetPrivateUserModel> _privateUserReader;
		private readonly IApiModifier<User, RegisterUserModel, GetPrivateUserModel> _privateUserModifier;
		private readonly IApiLoginFacilitator<LoginModel, GetPrivateUserModel> _loginFacilitator;
		private readonly IApiReader<GetArticleModel> _articleReader;
		private readonly IApiReader<GetCommentModel> _commentReader;
		private readonly IApiReader<GetBoardModel> _boardReader;

		public UsersController(
			IJwtService jwtService,
			IApiReader<GetPublicUserModel> publicUserReader,
			IApiReader<GetPrivateUserModel> privateUserReader,
			IApiModifier<User, RegisterUserModel, GetPrivateUserModel> privateUserModifier,
			IApiLoginFacilitator<LoginModel, GetPrivateUserModel> loginFacilitator,
			IApiReader<GetArticleModel> articleReader,
			IApiReader<GetCommentModel> commentReader,
			IApiReader<GetBoardModel> boardReader
			)
		{
			_jwtService = jwtService;
			_publicUserReader = publicUserReader;
			_privateUserReader = privateUserReader;
			_privateUserModifier = privateUserModifier;
			_loginFacilitator = loginFacilitator;
			_articleReader = articleReader;
			_commentReader = commentReader;
			_boardReader = boardReader;
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


		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel viewModel)
		{
			var loginModel = viewModel.PostModel;
			GetPrivateUserModel userResponse = await _loginFacilitator.GetUserByCredentialsAsync(loginModel);

			return RedirectToAction("Me");
		}

		public async Task<ViewResult> Me()
		{
			GetPrivateUserModel privateModel = await _privateUserReader.GetEndpointAsync($"{USER_ENDPOINT}/me", 0);
			var model = new PrivateUserDetailsViewModel { GetModel = privateModel };

			return View(model);
		}

		public async Task<ActionResult<PublicUserDetailsViewModel>> Details(int id)
		{
			var user = await _publicUserReader.GetEndpointAsync(USER_ENDPOINT, id);

			var returnModel = new PublicUserDetailsViewModel { GetModel = user };
			return View(returnModel);
		}

		public async Task<ActionResult<UsersListViewModel>> List(int pageNumber, int pageSize)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var users = await _publicUserReader.GetEndpointAsync(USER_ENDPOINT, pagingParams);

			var returnModel = new UsersListViewModel { GetModels = users };
			return View(returnModel);
		}

		public async Task<ActionResult<UserArticlesListView>> Articles(int userId)
		{
			var user = await _publicUserReader.GetEndpointAsync(USER_ENDPOINT, userId);

			// TODO: refactor to service and null checks and all that jazz
			var articles = await TaskHelper.RunConcurrentTasksAsync(user.ArticleIds, articleId => _articleReader.GetEndpointAsync("Articles", articleId));
			var model = new UserArticlesListView { GetModels = articles };
			return View(model);
		}

		public async Task<ActionResult<UserCommentsListView>> Comments(int userId)
		{
			var user = await _publicUserReader.GetEndpointAsync(USER_ENDPOINT, userId);

			var comments = await TaskHelper.RunConcurrentTasksAsync(user.CommentIds, commentId => _commentReader.GetEndpointAsync("Comments", commentId));
			var model = new UserCommentsListView { GetModels = comments };
			return View(model);
		}

		public async Task<ActionResult<UserSavedView>> Saved()
		{
			GetPrivateUserModel privateModel = await _privateUserReader.GetEndpointAsync($"{USER_ENDPOINT}/me", 0);

			var articles = await TaskHelper.RunConcurrentTasksAsync(privateModel.SavedArticles, articleId => _articleReader.GetEndpointAsync("Articles", articleId));
			var comments = await TaskHelper.RunConcurrentTasksAsync(privateModel.SavedComments, commentId => _commentReader.GetEndpointAsync("Comments", commentId));
			var model = new UserSavedView { SavedArticles = articles, SavedComments = comments };
			return View(model);
		}

		public async Task<ActionResult<UserBoardsView>> Boards()
		{
			GetPrivateUserModel privateModel = await _privateUserReader.GetEndpointAsync($"{USER_ENDPOINT}/me", 0);

			var boardsSubscribed = await TaskHelper.RunConcurrentTasksAsync(privateModel.BoardsSubscribed, boardId => _boardReader.GetEndpointAsync("Boards", boardId));
			var boardsModerating = await TaskHelper.RunConcurrentTasksAsync(privateModel.BoardsModerating, boardId => _boardReader.GetEndpointAsync("Boards", boardId));
			var model = new UserBoardsView { BoardsModerating = boardsModerating, BoardsSubscribed = boardsSubscribed };
			return View(model);
		}
	}
}
