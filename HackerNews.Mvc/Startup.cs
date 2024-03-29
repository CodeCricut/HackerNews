using HackerNews.Application;
using HackerNews.Domain;
using HackerNews.Infrastructure;
using HackerNews.Mvc.Configuration;
using HackerNews.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Mvc
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDomain(Configuration);
			services.AddInfrastructure(Configuration);
			services.AddApplication();
			services.AddWeb(Configuration);
			services.AddMvcProject(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.ConfigureApp(env);
		}
	}
}
