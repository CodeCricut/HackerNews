using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HackerNews.Helpers
{
	public class CookieUserManager : ICookieUserManager
	{
		private HttpContext _httpContext;

		public CookieUserManager(IHttpContextAccessor httpContextAccessor)
		{
			_httpContext = httpContextAccessor.HttpContext;
		}

		public async Task LogInAsync(GetPrivateUserModel user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim("FirstName", user.FirstName),
				new Claim("LastName", user.LastName),
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = true
			};

			await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}

		public async Task LogOutAsync()
		{
			await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
