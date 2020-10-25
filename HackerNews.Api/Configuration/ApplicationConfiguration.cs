using HackerNews.Api.Pipeline.Extensions;
using HackerNews.Api.Pipeline.Middleware;
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

			if (env.IsDevelopment())
			{
				// create the db if it doesn't exist
				dbContext.Database.EnsureCreated();

				
			}
			app.UseHttpsRedirection();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint($"/swagger/v1/swagger.json", "HackerNews API V1");
			});

			app.UseRouting();
			app.UseCors("DefaultCorsPolicy");

			app.UseApiExceptionHandler();
			app.UseMiddleware<JwtMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			return app;
		}
	}
}
