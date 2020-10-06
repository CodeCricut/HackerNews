using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers;
using HackerNews.Helpers.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class UsersController : Controller
	{
		private static readonly string USER_ENDPOINT = "users";
		private readonly ICookieService _cookieService;
		private readonly IApiReader<GetPublicUserModel> _publicUserReader;
		private readonly IApiReader<GetPrivateUserModel> _privateUserReader;
		private readonly IApiModifier<User, RegisterUserModel, GetPrivateUserModel> _privateUserModifier;
		private readonly IApiLoginFacilitator<LoginModel, GetPrivateUserModel> _loginFacilitator;

		public UsersController(
			ICookieService cookieService, 
			IApiReader<GetPublicUserModel> publicUserReader,
			IApiReader<GetPrivateUserModel> privateUserReader,
			IApiModifier<User, RegisterUserModel, GetPrivateUserModel> privateUserModifier,
			IApiLoginFacilitator<LoginModel, GetPrivateUserModel> loginFacilitator)
		{
			_cookieService = cookieService;
			_publicUserReader = publicUserReader;
			_privateUserReader = privateUserReader;
			_privateUserModifier = privateUserModifier;
			_loginFacilitator = loginFacilitator;
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
			GetPrivateUserModel addedUser = await _privateUserModifier.PostEndpointAsync($"{USER_ENDPOINT}/register", registerModel);
			return RedirectToAction("Login");
		}


		public ViewResult Login()
		{
			return View(new LoginModel());
		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginModel authRequest)
		{
			GetPrivateUserModel userResponse = await _loginFacilitator.GetUserByCredentialsAsync(authRequest);

			// TODO: refactor to JWT service
			_cookieService.Set("JWT", userResponse.JwtToken, 60);

			return RedirectToAction("Me");
		}

		public async Task<ViewResult> Me()
		{
			GetPrivateUserModel privateModel = await _privateUserReader.GetEndpointAsync($"{USER_ENDPOINT}/me", 0);
			return View(privateModel);
		}
	}
}
