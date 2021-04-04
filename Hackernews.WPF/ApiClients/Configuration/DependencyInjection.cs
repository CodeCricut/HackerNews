﻿using Hackernews.WPF.ApiClients;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hackernews.WPF.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWPF(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging();

			services.AddSingleton<MainWindow>();
			services.AddSingleton<LoginWindow>();

			services.AddHttpClient();

			services.AddHttpClient<IApiClient, ApiClient>(config =>
			{
				// TODO: put in config file
				string baseUrl = "https://localhost:44300/api/";
				config.BaseAddress = new System.Uri(baseUrl);
			});

			return services;
		}
	}
}
