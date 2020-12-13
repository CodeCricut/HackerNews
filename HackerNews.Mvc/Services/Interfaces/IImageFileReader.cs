using HackerNews.Domain.Common.Models.Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services.Interfaces
{
	public interface IImageFileReader
	{
		PostImageModel ConvertImageFileToImageModel(IFormFile file, string title, string description);
		PostImageModel ConvertImageFileToImageModel(IFormFile file);
	}
}
