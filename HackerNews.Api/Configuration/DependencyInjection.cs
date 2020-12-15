using HackerNews.Api.Configuration;
using HackerNews.Api.Helpers.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace HackerNews.Api
{
	static class DependencyInjection
	{
		/// <summary>
		/// Add the necessary services pertaining to the API to the container.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(opt =>
			{
				opt.AddPolicy(name: "DefaultCorsPolicy",
					builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});

			services.AddControllers(opt => opt.Filters.Add(typeof(AnalysisAsyncActionFilter)));

			services.ConfigureSwaggerGenForApp();

			services.AddMvcCore();

			return services;
		}
	}
}
