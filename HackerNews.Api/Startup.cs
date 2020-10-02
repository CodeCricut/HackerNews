using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Api.Helpers.EntityServices.Default;
using HackerNews.Api.Helpers.Filters;
using HackerNews.Api.Helpers.Middleware;
using HackerNews.Api.Helpers.StartupExtensions;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF;
using HackerNews.EF.Repositories;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System.Linq;

namespace HackerNews.Api
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
			services.AddCors(opt =>
			{
				opt.AddPolicy(name: "DefaultCorsPolicy",
					builder => builder.AllowAnyOrigin());
			});

			services.AddDbContext<HackerNewsContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("HackerNews")));

			services.AddAutoMapper(typeof(Startup));

			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

			// Entity-related services.
			services.AddEntityRepositories()
					.AddEntityServices()
					.AddVoteableEntityServices()
					.AddUserServices()
					.AddBoardServices();

			// used for querying actions
			services.AddOData();

			services.AddControllers(opt => opt.Filters.Add(typeof(AnalysisAsyncActionFilter)));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HackerNewsContext dbContext)
		{
			app.UseApiExceptionHandler();

			if (env.IsDevelopment())
			{
				// create the db if it doesn't exist
				dbContext.Database.EnsureCreated();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors("DefaultCorsPolicy");

			app.UseMiddleware<JwtMiddleware>();
			app.UseMiddleware<DeveloperMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapODataRoute("odata", "entities", GetEdmModel());
				endpoints.EnableDependencyInjection();

				// enable the OData functionality we want
				endpoints.Expand().Select().Filter().Count().OrderBy();
			});
		}

		/* The Entity Data Model (EDM) is a set of concepts 
		 * that describe the structure of data, regardless of its stored form. 
		 * The standard way of representing data is used by OData to better understand the data.
		 */
		private static IEdmModel GetEdmModel()
		{
			var builder = new ODataConventionModelBuilder();
			builder.EntitySet<Article>("Articles");
			builder.EntitySet<Comment>("Comments");
			builder.EntitySet<User>("Users");
			builder.EntitySet<Board>("Boards");

			return builder.GetEdmModel();
		}
	}
}
