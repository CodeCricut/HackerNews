using HackerNews.Domain.Common.Models.Images;
using Microsoft.AspNetCore.Http;

namespace HackerNews.Mvc.Services.Interfaces
{
	public interface IImageFileReader
	{
		/// <summary>
		/// Convert an image file, usually posted through a form, to a <see cref="PostImageModel"/>.
		/// </summary>
		PostImageModel ConvertImageFileToImageModel(IFormFile file, string title, string description);
		/// <summary>
		/// Convert an image file, usually posted through a form, to a <see cref="PostImageModel"/>.
		/// </summary>
		PostImageModel ConvertImageFileToImageModel(IFormFile file);
	}
}
