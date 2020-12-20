using HackerNews.Mvc.Pipeline.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace HackerNews.Mvc.Configuration
{
	public static class ApplicationConfiguration
	{
		public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMvcExceptionHandler();

			if (!env.IsDevelopment())
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});

			return app;
		}
	}
}
