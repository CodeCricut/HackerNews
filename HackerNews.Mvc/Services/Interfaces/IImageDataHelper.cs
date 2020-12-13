using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services.Interfaces
{
	public interface IImageDataHelper
	{
		string ConvertImageDataToDataUrl(byte[] imageData, string contentType);
	}
}
