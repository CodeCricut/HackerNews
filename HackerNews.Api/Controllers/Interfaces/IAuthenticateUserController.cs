using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	interface IAuthenticateUserController
	{
		Task<ActionResult<Jwt>> GetJwt(LoginModel loginModel);
		Task<ActionResult<GetPrivateUserModel>> GetAuthenticatedUser();
	}
}
