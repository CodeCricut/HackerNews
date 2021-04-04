using AutoMapper;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackerNews.Domain
{
	public static class DepenedencyInjection
	{
		public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
			services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));

			return services;
		}
	}
}
