using HackerNews.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

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
		/// Generated from the HttpContext.Items["UserID"]
		/// </summary>
		public int UserId
		{
			get
			{
				var userId = _httpContextAccessor.HttpContext.Items["UserId"];
				if (userId != null) return (int)userId;
				return -1;
			}
		}
	}
}
