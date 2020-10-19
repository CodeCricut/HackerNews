using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.Jwt
{
	[Route("api/jwt")]
	public class DefaultJwtController : JwtController<User, LoginModel>
	{
		public DefaultJwtController(IJwtService<User, LoginModel> jwtService) : base(jwtService)
		{
		}
	}
}
