using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Hackernews.WPF.Configuration
{
	public static class AppConfiguration
	{
		public static IConfiguration GetServiceConfiguration()
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json", 
				optional: false, reloadOnChange: true)
				.Build();

			return config;
		}
	}
}
