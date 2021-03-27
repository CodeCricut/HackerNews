using HackerNews.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace HackerNews.Web.Services
{
	class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		// Must be registered as service
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
				try
				{
					var user = _httpContextAccessor.HttpContext.User;
					string userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
					//string userId = user.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;
					if (!string.IsNullOrEmpty(userId)) return Int32.Parse(userId);
				}
				catch (Exception)
				{
				}
				return -1;
			}
		}
	}
}
