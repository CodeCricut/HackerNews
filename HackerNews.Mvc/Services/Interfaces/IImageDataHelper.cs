namespace HackerNews.Mvc.Services.Interfaces
{
	public interface IImageDataHelper
	{
		string ConvertImageDataToDataUrl(byte[] imageData, string contentType);
	}
}
