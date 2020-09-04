using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Api.DB_Helpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Newtonsoft.Json;

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
			services.AddDbContext<HackerNewsContext>(options =>
				// connections strings are configured in appsettings.json
				options.UseSqlServer(Configuration.GetConnectionString("HackerNews")));
			services.AddScoped<IArticleRepository, ArticleRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
			services.AddScoped<IArticleHelper, ArticleHelper>();
			services.AddScoped<ICommentHelper, CommentHelper>();

			// we have to add the startup type param to fix some versioning issues
			services.AddAutoMapper(typeof(Startup));

			// used for querying actions
			services.AddOData();
			//services.AddMvc();

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseExceptionHandler(a => a.Run(async context => 
			{
				var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
				var exception = exceptionHandlerPathFeature.Error; // Your exception
				var code = 500; // Internal Server Error by default

				if (exception is NotFoundException) code = 404; // Not Found
				else if (exception is UnauthorizedException) code = 401; // Unauthorized
				else if (exception is InvalidPostException) code = 400; // Bad Request

				var result = JsonConvert.SerializeObject(new ErrorResponse(exception));

				context.Response.StatusCode = code;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsync(result);
			}));

			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				// look into this, this is a guess
				endpoints.MapODataRoute("odata", "api", GetEdmModel());
				// enable the OData functionality we want
				endpoints.Expand().Select().Filter().Count().OrderBy();
			});

			//app.UseMvc(b =>
			//{
			//	b.MapODataServiceRoute("odata", "odata", GetEdmModel());
			//});
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

			return builder.GetEdmModel();
		}
	}
}
