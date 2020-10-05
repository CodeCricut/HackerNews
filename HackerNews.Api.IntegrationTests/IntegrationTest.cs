using AutoMapper;
using HackerNews.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace HackerNews.Api.IntegrationTests
{
	public class IntegrationTest : WebApplicationFactory<Startup>
	{
		public Guid guid = Guid.NewGuid();
		public HttpClient Client { get; set; }
		public IMapper Mapper { get; set; }

		public HackerNewsContext DbContext { get; set; }

		public static InMemoryDatabaseRoot DatabaseRoot = new InMemoryDatabaseRoot();
		public IntegrationTest()
		{
			Client = CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("http://localhost/api/")
			});
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureTestServices(services =>
			{
				// try remove existing DB
				var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HackerNewsContext>));
				if (descriptor != null) services.Remove(descriptor);

				// add in memory db
				services.AddDbContext<HackerNewsContext>(opt => { opt.UseInMemoryDatabase("HackerNewsInMemory"); }
				// ensure each instance of this class uses a new in memory DB
				//, DatabaseRoot)
				);

				// create a temporary service provider using current configuration
				var sp = services.BuildServiceProvider();

				using var scope = sp.CreateScope();
				// verify the DB was created
				var scopedServices = scope.ServiceProvider;
				var db = scopedServices.GetRequiredService<HackerNewsContext>();

				db.Database.EnsureCreated();

				// db.SeedDatabase();

				DbContext = db;

				Mapper = scopedServices.GetRequiredService<IMapper>();
			});
		}

		public void ResetDatabaseContext()
		{
			using (var scope = Services.CreateScope())
			{
				var ctx = scope.ServiceProvider.GetRequiredService<HackerNewsContext>();

				ctx.DisposeDatabase();
				ctx.SeedDatabase();
			}
		}
	}
}
