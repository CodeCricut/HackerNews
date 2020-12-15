namespace HackerNews.Mvc.Services.Interfaces
{
	public interface IImageDataHelper
	{
		/// <summary>
		/// Converts image data to a data url able to be displayed on a webpage (for example).
		/// </summary>
		/// <param name="imageData"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		string ConvertImageDataToDataUrl(byte[] imageData, string contentType);
	}
}
