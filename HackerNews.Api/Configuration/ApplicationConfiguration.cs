using HackerNews.Api.Helpers.Middleware;
using HackerNews.Api.Helpers.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HackerNews.Api
{
	public static class ApplicationConfiguration
	{
		public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env, DbContext dbContext)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			app.UseApiExceptionHandler();

			if (env.IsDevelopment())
			{
				// create the db if it doesn't exist
				dbContext.Database.EnsureCreated();

				// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
				// specifying the Swagger JSON endpoint.
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint($"/swagger/v1/swagger.json", "HackerNews API V1");
				});
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors("DefaultCorsPolicy");

			app.UseMiddleware<DeveloperMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			return app;
		}
	}
}
