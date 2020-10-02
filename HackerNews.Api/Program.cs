using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackerNews.Api
{
	public class Program
	{
		// Just like console apps, this is the entry point of the application.
		// The application will be run with settings from Properties/launchSettings.json
		// If you use dotnet run, then the application will be run using the HackerNews.Api project section of launchSettings.
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		// This method is required when using EF Core.
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			// Behind the scenes, the default builder is going to setup some of the default services, such as the configuration service.
			// Look here to see what CreateDefaultBuilder does: https://dotnetcoretutorials.com/2019/07/31/what-does-the-createdefaultbuilder-method-do-in-asp-net-core/
			Host.CreateDefaultBuilder(args)
			.ConfigureLogging(logging =>
			{
				logging.ClearProviders();
				logging.AddConsole();
				logging.AddDebug();
			})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
