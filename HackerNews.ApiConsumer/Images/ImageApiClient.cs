using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Images;
using System.Threading.Tasks;

namespace HackerNews.ApiConsumer.Images
{
	public interface IImageApiClient
	{
		Task<GetImageModel> GetImageAsync(int imageId);
	}

	internal class ImageApiClient : IImageApiClient
	{
		private readonly IApiClient _apiClient;

		public ImageApiClient(IApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public Task<GetImageModel> GetImageAsync(int imageId)
			=> _apiClient.GetAsync<GetImageModel>(imageId, "images");
	}
}
