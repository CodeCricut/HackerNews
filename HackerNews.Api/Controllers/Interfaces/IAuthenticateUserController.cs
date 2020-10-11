using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IAuthenticateUserController
	{
		public Task<ActionResult<GetPrivateUserModel>> AuthenticateAsync([FromBody] LoginModel model);
		public Task<ActionResult<GetPrivateUserModel>> GetPrivateUserAsync();
	}
}
