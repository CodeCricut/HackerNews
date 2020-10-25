using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class ReadPublicUserController : ApiController, IReadEntityController<User, GetPublicUserModel>
	{
		public Task<ActionResult<PaginatedList<GetPublicUserModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult<GetPublicUserModel>> GetByIdAsync(int key)
		{
			throw new System.NotImplementedException();
		}
	}
}
