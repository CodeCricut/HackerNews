using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class AuthenticateUserController : ControllerBase, IAuthenticateUserController
	{
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuthService;

		public AuthenticateUserController(IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService)
		{
			_userAuthService = userAuthService;
		}

		[HttpPost]
		public async Task<ActionResult<GetPrivateUserModel>> AuthenticateAsync([FromBody] LoginModel model)
		{
			var response = await _userAuthService.AuthenticateAsync(model);

			if (response == null) throw new NotFoundException("Username or password is incorrect.");

			return Ok(response);
		}

		[HttpGet("me")]
		[Authorize]
		public async Task<ActionResult<GetPrivateUserModel>> GetPrivateUserAsync()
		{
			var privateUser = await _userAuthService.GetAuthenticatedReturnModelAsync();

			if (privateUser == null) return StatusCode(StatusCodes.Status500InternalServerError);

			return Ok(privateUser);
		}
	}
}
