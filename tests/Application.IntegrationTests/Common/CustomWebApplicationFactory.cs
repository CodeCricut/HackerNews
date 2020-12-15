using HackerNews.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Application.IntegrationTests
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			var projectDir = Directory.GetCurrentDirectory();
			var configPath = Path.Combine(projectDir, "appsettings.json");

			// Add the appsettings.json in this project to the app.
			builder.ConfigureAppConfiguration(config =>
			{
				var integrationConfig = new ConfigurationBuilder()
					.AddJsonFile(configPath)
					.Build();

				config.AddConfiguration(integrationConfig);
			});

			builder.ConfigureServices(services =>
			{
				// Configured to use InMemoryDB as per the integration project DI
				var sp = services.BuildServiceProvider();
				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = (HackerNewsContext)scopedServices.GetRequiredService<DbContext>();
					var logger = scopedServices
						.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

					// Reset the database
					db.Database.EnsureDeleted();
					db.Database.EnsureCreated();

					try
					{
						// Seed the database
						db.InitializeForTests();
					}
					catch (Exception ex)
					{
						logger.LogError(ex, "An error occurred seeding the " +
							"database with test messages. Error: {Message}", ex.Message);
					}
				}
			});
		}
	}
}
