using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.TUI.Configuration
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
