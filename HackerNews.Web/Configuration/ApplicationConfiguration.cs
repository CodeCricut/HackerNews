using HackerNews.Web.Pipeline.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

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
