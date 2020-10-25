using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.JwtControllers
{
	[Route("api/jwt")]
	public class DefaultJwtController : ApiController, IJwtController<LoginModel>
	{
		private readonly IJwtGeneratorService _jwtGeneratorService;

		public DefaultJwtController(IJwtGeneratorService jwtGeneratorService)
		{
			_jwtGeneratorService = jwtGeneratorService;
		}

		public async Task<ActionResult<Jwt>> GetTokenAsync([FromBody] LoginModel loginModel)
		{
			return await _jwtGeneratorService.GenererateJwtFromLoginModelAsync(loginModel);
		}
	}
}
