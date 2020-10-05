using AutoMapper;
using HackerNews.Api.Helpers.Filters;
using HackerNews.Api.Helpers.Middleware;
using HackerNews.Api.Helpers.StartupExtensions;
using HackerNews.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace HackerNews.Api
{
	public class Startup
	{
		private static string SWAGGER_DOC_NAME = "v1";
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


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

			services.AddControllers(opt => opt.Filters.Add(typeof(AnalysisAsyncActionFilter)));

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc(SWAGGER_DOC_NAME, new OpenApiInfo
				{
					Version = "v1",
					Title = "HackerNews API",
					Description = "An API for a social app similar to Reddit.com, including support for users, article posts, comments, and boards to group like minded individuals.",
					Contact = new OpenApiContact
					{
						Name = "A. Joseph Richerson",
						Email = string.Empty
					}
				});

				// Set the comments path for the Swagger JSON and UI.**
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				opt.IncludeXmlComments(xmlPath);
			});


			services.AddMvcCore();//.AddApiExplorer();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HackerNewsContext dbContext)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			app.UseApiExceptionHandler();

			if (env.IsDevelopment())
			{
				// create the db if it doesn't exist
				dbContext.Database.EnsureCreated();

				// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
				// specifying the Swagger JSON endpoint.
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint($"/swagger/{SWAGGER_DOC_NAME}/swagger.json", "HackerNews API V1");
				});
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors("DefaultCorsPolicy");

			app.UseMiddleware<JwtMiddleware>();
			app.UseMiddleware<DeveloperMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
