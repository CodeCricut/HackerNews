using HackerNews.Web.Pipeline.Middleware;
using Microsoft.AspNetCore.Builder;

namespace HackerNews.Web.Configuration
{
	public static class ApplicationConfiguration
	{
		/// <summary>
		/// Configure the pipeline to use the necessary middleware, filters, etc. pertaining to common Web functionality.
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder ConfigureWebLayer(this IApplicationBuilder app)
		{
			app.UseMiddleware<JwtMiddleware>();
			return app;
		}
	}
}
