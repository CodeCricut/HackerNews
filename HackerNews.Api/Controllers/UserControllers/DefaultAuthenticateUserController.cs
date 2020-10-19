using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
