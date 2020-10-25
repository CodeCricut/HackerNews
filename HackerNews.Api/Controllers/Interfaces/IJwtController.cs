using HackerNews.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IJwtController<TLoginModel>
	{
		public Task<ActionResult<Jwt>> GetTokenAsync([FromBody] TLoginModel loginModel);
	}
}
