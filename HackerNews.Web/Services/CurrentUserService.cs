using HackerNews.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace HackerNews.Web.Services
{
	class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		/// <summary>
		/// Generated from "id" claim of the current IdentityPrincipal. 
		/// </summary>
		public int UserId
		{
			get
			{
				string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;

				if (! string.IsNullOrEmpty(userId)) return Int32.Parse(userId);
				return -1;
			}
		}
	}
}
