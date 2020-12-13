using AutoMapper;
using HackerNews.Domain.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackerNews.Domain
{
	public static class DepenedencyInjection
	{
		public static IServiceCollection AddDomain(this IServiceCollection services)
		{
			//var mapperConfig = new MapperConfiguration(cfg =>
			//{
			//	cfg.AddProfile<MappingProfile>();
			//});

			services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
			return services;
		}
	}
}
