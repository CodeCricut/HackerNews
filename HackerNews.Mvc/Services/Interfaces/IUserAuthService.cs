using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services.Interfaces
{
	/// <summary>
	/// Responsible for logging the user in and out.
	/// </summary>
	public interface IUserAuthService
	{
		Task<Jwt> LogIn(LoginModel loginModel);
		Task LogOut();
	}
}
