using HackerNews.Web.Pipeline.Middleware;
using Microsoft.AspNetCore.Builder;

namespace HackerNews.Web.Configuration
{
	public static class ApplicationConfiguration
	{
		public static IApplicationBuilder ConfigureWebLayer(this IApplicationBuilder app)
		{
			app.UseMiddleware<JwtMiddleware>();
			return app;
		}
	}
}
