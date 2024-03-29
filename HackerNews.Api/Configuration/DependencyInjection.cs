﻿using HackerNews.Api.Configuration;
using HackerNews.Api.Helpers.Filters;
using HackerNews.Api.Services;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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

			// Configure Identity to use JWT
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // remove default claims
			services
				// Essentially configure the application to use the JWT scheme for everything
				.AddAuthentication(opt =>
				{
					opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				// For any incoming request that has to be authenticated/authorized, reconstruct the user
				// by decrypting the JWT header.
				.AddJwtBearer(cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;
					cfg.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = jwtTokenConfig.Issuer,
						ValidAudience = jwtTokenConfig.Issuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Key)),
						ClockSkew = TimeSpan.Zero // remove delay of token when expire
					};
				});

			services.AddCors(opt =>
			{
				opt.AddPolicy(name: "DefaultCorsPolicy",
					builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});

			services.AddControllers(opt => opt.Filters.Add(typeof(AnalysisAsyncActionFilter)));

			services.ConfigureSwaggerGenForApp();

			services.AddMvcCore();

			services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();

			return services;
		}
	}
}
