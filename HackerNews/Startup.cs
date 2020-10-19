using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.ApiServices.Default.ArticleServices;
using HackerNews.Helpers.ApiServices.Default.BoardServices;
using HackerNews.Helpers.ApiServices.Default.CommentServices;
using HackerNews.Helpers.ApiServices.Default.UserServices;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HackerNews
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

			services.AddHttpClient();

			services.AddScoped<IApiReader, ApiReader>();

			//services.AddScoped<IApiReader<GetBoardModel>, BoardApiReader>();
			services.AddScoped<IApiModifier<Board, PostBoardModel, GetBoardModel>, BoardApiModifier>();
			services.AddScoped<IApiBoardModeratorAdder, ApiBoardModeratorAdder>();

			//services.AddScoped<IApiReader<GetArticleModel>, ArticleApiReader>();
			services.AddScoped<IApiModifier<Article, PostArticleModel, GetArticleModel>, ArticleApiModifier>();

			//services.AddScoped<IApiReader<GetCommentModel>, CommentApiReader>();
			services.AddScoped<IApiModifier<Comment, PostCommentModel, GetCommentModel>, CommentApiModifier>();

			//services.AddScoped<IApiReader<GetPublicUserModel>, PublicUserApiReader>();
			//services.AddScoped<IApiReader<GetPrivateUserModel>, PrivateUserApiReader>();
			services.AddScoped<IApiModifier<User, RegisterUserModel, GetPrivateUserModel>, PrivateUserApiModifier>();
			services.AddScoped<IApiLoginFacilitator<LoginModel, GetPrivateUserModel>, UserApiLoginFacilitator>();
			services.AddScoped<IApiUserSaver<Article, GetPrivateUserModel>, ApiUserSaver>();
			services.AddScoped<IApiUserSaver<Comment, GetPrivateUserModel>, ApiUserSaver>();

			services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
			services.AddTransient<ICookieService, CookieService>();
			services.AddTransient<IJwtService, JwtCookieService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
