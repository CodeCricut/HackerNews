using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HackerNews.Mvc.Controllers
{
	public class DownloadsController : FrontendController
	{
		private readonly IWebHostEnvironment _environment;

		public DownloadsController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public FileContentResult Wpf()
		{
			try
			{
				var file = System.IO.File.ReadAllBytes(@"C:\Users\ajori\Pictures\Saved Pictures\walrus.jpg");
				return new FileContentResult(file, "image/jpeg");
			}
			catch (Exception e)
			{

				throw;
			}
		}
	}
}
