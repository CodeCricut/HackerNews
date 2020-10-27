using HackerNews.Api;
using HackerNews.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Application.IntegrationTests.Common
{
	public abstract class AppIntegrationTest
		: IDisposable
	//, IClassFixture<CustomWebApplicationFactory<Startup>>,
	// CustomWebApplicationFactory<Startup>
	{
		public AppIntegrationTest(/* CustomWebApplicationFactory<Startup> factory */)
		{
			Factory = new CustomWebApplicationFactory<Startup>();
			Client = Factory.CreateClient();
		}

		public CustomWebApplicationFactory<Startup> Factory { get; }
		public HttpClient Client { get; private set; }

		public void Dispose()
		{
			using var scope = Factory.Services.CreateScope();
			var context = (HackerNewsContext)scope.ServiceProvider.GetService<DbContext>();
			context.ClearDatabase();
			context.InitializeForTests();
		}
	}
}
