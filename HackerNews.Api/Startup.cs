using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Api.Helpers.EntityServices.Default;
using HackerNews.Api.Helpers.Middleware;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
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

			services.AddScoped<IEntityRepository<Article>, ArticleRepository>();
			services.AddScoped<IEntityRepository<Comment>, CommentRepository>();
			services.AddScoped<IEntityRepository<User>, UserRepository>();

			services.AddScoped<ArticleService, DefaultArticleService>();
			services.AddScoped<CommentService, DefaultCommentService>();
			services.AddScoped<UserService, DefaultUserService>();

			services.AddScoped<IVoteableEntityService<Article>, DefaultArticleService>();
			services.AddScoped<IVoteableEntityService<Comment>, DefaultCommentService>();

			services.AddScoped<UserAuthService, DefaultUserAuthService>();
			services.AddScoped<UserSaverService, DefaultUserSaverService>();

			// used for querying actions
			services.AddOData();

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HackerNewsContext dbContext)
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
				// create the db if it doesn't exist
				dbContext.Database.EnsureCreated();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors("DefaultCorsPolicy");

			app.UseMiddleware<JwtMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapODataRoute("odata", "api", GetEdmModel());
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

			return builder.GetEdmModel();
		}
	}
}
