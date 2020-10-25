using HackerNews.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HackerNews.Api.Services
{
	public class CurrentUserService : ICurrentUserService
	{
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => (int) _httpContextAccessor.HttpContext.Items["UserId"];
    }
}
