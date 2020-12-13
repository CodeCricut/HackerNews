using HackerNews.Domain.Common.Models.Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services.Interfaces
{
	public class ImageFileReader : IImageFileReader
	{
		public PostImageModel ConvertImageFileToImageModel(IFormFile file, string title, string description)
		{
			// Copy the image data to the image object
			PostImageModel img = new PostImageModel();
			using MemoryStream ms = new MemoryStream();
			file.CopyTo(ms);
			img.ImageData = ms.ToArray();

			// Add other image fields
			img.ImageTitle = title;
			img.ImageDescription = description;

			ms.Close();

			return img;
		}

		public PostImageModel ConvertImageFileToImageModel(IFormFile file)
		{
			return ConvertImageFileToImageModel(file, "Image Title", "Image Description");
		}
	}
}
