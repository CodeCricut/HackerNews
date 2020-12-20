using HackerNews.Api;
using HackerNews.Domain.Entities;
using HackerNews.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Application.IntegrationTests.Common
{
	/// <summary>
	/// A base integration test.
	/// </summary>
	public abstract class AppIntegrationTest
		: IDisposable
	{
		/// <summary>
		/// Bootstraps the app into memory through <see cref="HackerNews.Api.Startup"/>. <seealso cref="CustomWebApplicationFactory{TStartup}"/> 
		/// </summary>
		public CustomWebApplicationFactory<Startup> Factory { get; }
		public HttpClient Client { get; private set; }

		public AppIntegrationTest()
		{
			Factory = new CustomWebApplicationFactory<Startup>();
			Client = Factory.CreateClient();
		}

		public void Dispose()
		{
			using var scope = Factory.Services.CreateScope();
			var context = scope.ServiceProvider.GetService<HackerNewsContext>();
			var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
			context.ClearDatabase();
			context.InitializeForTestsAsync(userManager).GetAwaiter().GetResult();
		}
	}
}
