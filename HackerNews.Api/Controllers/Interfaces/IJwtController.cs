using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Application.Common.Models;
namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IJwtController<TLoginModel>
	{
		public Task<ActionResult<Jwt>> GetTokenAsync([FromBody] TLoginModel loginModel);
	}
}
