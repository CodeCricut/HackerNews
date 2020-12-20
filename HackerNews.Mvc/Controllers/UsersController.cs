using HackerNews.Application.Articles.Queries.GetArticlesByIds;
using HackerNews.Application.Boards.Queries.GetBoardsByIds;
using HackerNews.Application.Comments.Queries.GetCommentsByIds;
using HackerNews.Application.Images.Queries.GetImageById;
using HackerNews.Application.Users.Commands.AddImage;
using HackerNews.Application.Users.Commands.DeleteUser;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.Services.Interfaces;
using HackerNews.Mvc.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Controllers
{
	public class UsersController : FrontendController
	{
		private readonly IUserAuthService _userAuthService;
		private readonly IImageFileReader _imageFileReader;
		private readonly IImageDataHelper _imageDataHelper;

		public UsersController(IUserAuthService userAuthService, IImageFileReader imageFileReader, IImageDataHelper imageDataHelper)
		{
			_userAuthService = userAuthService;
			_imageFileReader = imageFileReader;
			_imageDataHelper = imageDataHelper;
		}

		public ViewResult Register()
		{
			// Todo: automatically intialize and put private setters on these post models.
			var model = new UserRegisterViewModel { RegisterModel = new RegisterUserModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
		{
			// Verify not logged in
			GetPrivateUserModel privateUser = null;
			try
			{
				privateUser = await Mediator.Send(new GetAuthenticatedUserQuery());
			}
			catch (NotFoundException) { }
			catch (UnauthorizedException) { }
			if (privateUser != null) return RedirectToAction("Login");

			// Regiser
			User registeredUser = await Mediator.Send(new RegisterUserCommand(viewModel.RegisterModel));

			// Login
			var loginModel = new LoginModel { Username = registeredUser.UserName, Password = viewModel.RegisterModel.Password };
			await _userAuthService.LogInAsync(loginModel);

			// Attatch image if present
			var file = Request.Form.Files.FirstOrDefault();
			if (file != null)
			{
				PostImageModel imageModel = _imageFileReader.ConvertImageFileToImageModel(file);
				await Mediator.Send(new AddUserImageCommand(imageModel, registeredUser.Id));
			}

			return RedirectToAction("Me");
		}

		public ViewResult Login()
		{
			var model = new UserLoginViewModel { LoginModel = new LoginModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromQuery] string ReturnUrl, UserLoginViewModel viewModel)
		{
			try
			{
				await _userAuthService.LogInAsync(viewModel.LoginModel);
				return RedirectToAction("Me");
			}
			catch
			{
				return RedirectToAction("Register");
			}
		}


		public async Task<IActionResult> Logout()
		{
			await _userAuthService.LogOutAsync();
			return RedirectToAction("Register");
		}

		[Authorize]
		public async Task<ViewResult> Me()
		{
			GetPrivateUserModel privateModel = await Mediator.Send(new GetAuthenticatedUserQuery());

			var pagingParams = new PagingParams { PageNumber = 1, PageSize = 10 };
			var articles = await Mediator.Send(new GetArticlesByIdsQuery(privateModel.ArticleIds, pagingParams));
			var comments = await Mediator.Send(new GetCommentsByIdsQuery(privateModel.CommentIds, pagingParams));

			string imageDataURL = "";
			if (privateModel.ProfileImageId > 0)
			{
				GetImageModel img = await Mediator.Send(new GetImageByIdQuery(privateModel.ProfileImageId));
				imageDataURL = _imageDataHelper.ConvertImageDataToDataUrl(img.ImageData, img.ContentType);
			}

			var model = new PrivateUserDetailsViewModel
			{
				User = privateModel,
				ArticlePage = new FrontendPage<GetArticleModel>(articles),
				CommentPage = new FrontendPage<GetCommentModel>(comments),
				ImageDataUrl = imageDataURL
			};

			return View(model);
		}

		public async Task<ActionResult<PublicUserDetailsViewModel>> Details(int id)
		{
			var user = await Mediator.Send(new GetPublicUserQuery(id));

			var pagingParams = new PagingParams { PageNumber = 1, PageSize = 10 };
			var articles = await Mediator.Send(new GetArticlesByIdsQuery(user.ArticleIds, pagingParams));
			var comments = await Mediator.Send(new GetCommentsByIdsQuery(user.CommentIds, pagingParams));

			string imageDataURL = "";
			if (user.ProfileImageId > 0)
			{
				GetImageModel img = await Mediator.Send(new GetImageByIdQuery(user.ProfileImageId));
				imageDataURL = _imageDataHelper.ConvertImageDataToDataUrl(img.ImageData, img.ContentType);
			}

			var returnModel = new PublicUserDetailsViewModel
			{
				User = user,
				ArticlePage = new FrontendPage<GetArticleModel>(articles),
				CommentPage = new FrontendPage<GetCommentModel>(comments),
				ImageDataUrl = imageDataURL
			};
			return View(returnModel);
		}


		public async Task<ActionResult<UserArticlesViewModel>> Articles(int userId, PagingParams pagingParams)
		{
			var user = await Mediator.Send(new GetPublicUserQuery(userId));
			var articles = await Mediator.Send(new GetArticlesByIdsQuery(user.ArticleIds, pagingParams));

			var model = new UserArticlesViewModel
			{
				ArticlePage = new FrontendPage<GetArticleModel>(articles),
				UserId = userId
			};
			return View(model);
		}

		public async Task<ActionResult<UserCommentsViewModel>> Comments(int userId, PagingParams pagingParams)
		{
			var user = await Mediator.Send(new GetPublicUserQuery(userId));
			var comments = await Mediator.Send(new GetCommentsByIdsQuery(user.CommentIds, pagingParams));

			var model = new UserCommentsViewModel { CommentPage = new FrontendPage<GetCommentModel>(comments) };
			return View(model);
		}

		[Authorize]
		public async Task<ActionResult<UserSavedViewModel>> Saved(PagingParams pagingParams)
		{
			var privateUser = await Mediator.Send(new GetAuthenticatedUserQuery());

			var articles = await Mediator.Send(new GetArticlesByIdsQuery(privateUser.SavedArticles, pagingParams));
			var comments = await Mediator.Send(new GetCommentsByIdsQuery(privateUser.SavedComments, pagingParams));

			var model = new UserSavedViewModel
			{
				SavedArticlesPage = new FrontendPage<GetArticleModel>(articles),
				SavedCommentsPage = new FrontendPage<GetCommentModel>(comments)
			};
			return View(model);
		}

		[Authorize]
		public async Task<ActionResult<UserBoardsViewModel>> Boards(PagingParams pagingParams)
		{
			var privateUser = await Mediator.Send(new GetAuthenticatedUserQuery());

			var boardsSubscribed = await Mediator.Send(new GetBoardsByIdsQuery(privateUser.BoardsSubscribed, pagingParams));
			var boardsModerating = await Mediator.Send(new GetBoardsByIdsQuery(privateUser.BoardsModerating, pagingParams));

			var model = new UserBoardsViewModel
			{
				BoardsSubscribedPage = new FrontendPage<GetBoardModel>(boardsSubscribed),
				BoardsModeratingPage = new FrontendPage<GetBoardModel>(boardsModerating)
			};
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> Delete()
		{
			await Mediator.Send(new DeleteCurrentUserCommand());
			return RedirectToAction("me");
		}
	}
}
