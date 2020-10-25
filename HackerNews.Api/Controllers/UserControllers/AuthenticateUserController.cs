using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class AuthenticateUserController : ApiController, IAuthenticateUserController
	{
		private readonly IJwtGeneratorService _jwtGeneratorService;

		public AuthenticateUserController(IJwtGeneratorService jwtGeneratorService)
		{
			_jwtGeneratorService = jwtGeneratorService;
		}

		[Authorize]
		public async Task<ActionResult<GetPrivateUserModel>> GetAuthenticatedUser()
		{
			return Ok(await Mediator.Send(new GetAuthenticatedUserQuery()));
		}

		public async Task<ActionResult<Jwt>> GetJwt(LoginModel loginModel)
		{
			return await _jwtGeneratorService.GenererateJwtFromLoginModelAsync(loginModel);
		}
	}
}
