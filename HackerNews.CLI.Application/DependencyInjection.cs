﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackerNews.CLI.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCliApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
