using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services
{
	/// <summary>
	/// For making basic HTTP requests.
	/// </summary>
	public interface IGenericHttpClient
	{
		Task<TOut> GetRequest<TOut>(string uri);
		Task<TOut> PostRequestAsync<TIn, TOut>(string uri, TIn content);
	}

	public class GenericHttpClient : IGenericHttpClient
	{
		public async Task<TOut> GetRequest<TOut>(string uri)
		{
			using var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			using HttpResponseMessage response = await client.GetAsync(uri);

			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TOut>(responseBody);
		}

		public async Task<TOut> PostRequestAsync<TIn, TOut>(string uri, TIn content)
		{
			using var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));

			var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

			using HttpResponseMessage response = await client.PostAsync(uri, serialized);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TOut>(responseBody);
		}
	}
}
