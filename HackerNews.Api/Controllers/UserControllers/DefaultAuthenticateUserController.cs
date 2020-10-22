using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class DefaultAuthenticateUserController : AuthenticateUserController<GetPrivateUserModel, LoginModel>
	{
		public DefaultAuthenticateUserController(IAuthenticateUserService<LoginModel, GetPrivateUserModel> userAuthService) : base(userAuthService)
		{
		}
	}
}
