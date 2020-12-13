using HackerNews.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services
{
	public class ImageDataHelper : IImageDataHelper
	{
		public string ConvertImageDataToDataUrl(byte[] imageData, string contentType)
		{
			string imageBase64Data = Convert.ToBase64String(imageData);
			string imageDataURL = string.Format($"data:{contentType};base64,{{0}}", imageBase64Data);
			return imageDataURL;
		}
	}
}
