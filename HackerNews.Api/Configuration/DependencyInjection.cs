using HackerNews.Api.Configuration;
using HackerNews.Api.Helpers.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
			var jwtTokenConfig = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;

			//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			//	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			//	{
			//		// TODO: I think I need to bind the JWT secret here but IDK how...
			//		opt.RequireHttpsMetadata = true;
			//		opt.TokenValidationParameters = new TokenValidationParameters
			//		{
			//			ValidateIssuer = false,
			//			// ValidIssuer = jwtTokenConfig.Issuer,
			//			ValidateIssuerSigningKey = true,
			//			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
			//			// ValidAudience = jwtTokenConfig.Audience,
			//			// ValidateAudience = true,
			//			ValidateLifetime = true,
			//			// ClockSkew = TimeSpan.FromMinutes(1)
			//		};
			//	});

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
