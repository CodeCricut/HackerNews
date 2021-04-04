using HackerNews.Application;
using HackerNews.Domain;
using HackerNews.Infrastructure;
using HackerNews.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// Register services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDomain(Configuration);
			services.AddInfrastructure(Configuration);
			services.AddApplication();
			services.AddWeb(Configuration);
			services.AddApi(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContext dbContext)
		{
			app.ConfigureApp(env, dbContext);
		}
	}
}
