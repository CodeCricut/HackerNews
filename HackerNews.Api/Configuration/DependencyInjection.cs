using HackerNews.Api.Helpers.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;


namespace HackerNews.Api
{
	static class DependencyInjection
	{
		public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(opt =>
			{
				opt.AddPolicy(name: "DefaultCorsPolicy",
					builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});

			services.AddControllers(opt => opt.Filters.Add(typeof(AnalysisAsyncActionFilter)));

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo
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

				// Add JWT support to the UI
				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme."
				});

				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}

					}
				});
			});

			services.AddMvcCore();

			return services;
		}
	}
}
