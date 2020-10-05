using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Controllers
{
	public class UsersController : Controller
	{
		private readonly ICookieService _cookieService;
		private readonly ArticleApiConsumer _articleApi;
		private readonly UserApiConsumer _userApi;

		public UsersController(ICookieService cookieService, ArticleApiConsumer articleApi, UserApiConsumer userApi)
		{
			_cookieService = cookieService;
			_articleApi = articleApi;
			_userApi = userApi;
		}

		public IActionResult Index()
		{
			return View();
		}


		public ViewResult Register()
		{
			return View(new RegisterUserModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUserModel registerModel)
		{
			GetPublicUserModel addedUser = await _userApi.PostEndpointAsync("users/register", registerModel);


		}


		public ViewResult Login()
		{
			return View(new LoginModel());
		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginModel authRequest)
		{
			GetPrivateUserModel userResponse = await _userApi.GetUserByCredentialsAsync(authRequest);

			// TODO: refactor to JWT service
			_cookieService.Set("JWT", userResponse.JwtToken, 60);

			return RedirectToAction("Me");
		}

		public async Task<ViewResult> Me()
		{
			var jwtToken = _cookieService.Get("JWT");

			var privateModel = await _userApi.GetPrivateUserAsync(jwtToken);
			return View(privateModel);
		}


	}
}
