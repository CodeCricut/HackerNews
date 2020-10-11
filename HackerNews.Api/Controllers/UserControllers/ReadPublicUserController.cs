using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Base;
using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class ReadPublicUserController : ReadEntityController<User, GetPublicUserModel>
	{
		public ReadPublicUserController(IReadEntityService<User, GetPublicUserModel> readService) : base(readService)
		{
		}
	}
}
