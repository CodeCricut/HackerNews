using HackerNews.Api.Pipeline.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HackerNews.Api
{
	static class ApplicationConfiguration
	{
		/// <summary>
		/// Configure the pipeline, including calling subconfigurations.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="dbContext"></param>
		/// <returns></returns>
		public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env, DbContext dbContext)
		{
			app.UseApiExceptionHandler();

			if (env.IsDevelopment())
			{
				// Create the db if it doesn't exist.
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

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			return app;
		}
	}
}
