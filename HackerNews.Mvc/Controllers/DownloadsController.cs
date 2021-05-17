using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Mvc.Controllers
{
	public class DownloadsController : FrontendController
	{
		private readonly IWebHostEnvironment _environment;

		public DownloadsController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}
	}
}
