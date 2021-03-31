using Hackernews.WPF.Services;
using HackerNews.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWPF(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<MainWindow>();
			services.AddSingleton<ICurrentUserService, CurrentUserService>();
			return services;
		}
	}
}
