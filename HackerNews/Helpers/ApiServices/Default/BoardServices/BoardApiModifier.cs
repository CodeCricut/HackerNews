using HackerNews.Domain.Entities;
using HackerNews.Domain.Models.Board;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers.ApiServices.Default.BoardServices
{
	public class BoardApiModifier : ApiModifier<Board, PostBoardModel, GetBoardModel>
	{
		public BoardApiModifier(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService)
		{
		}
	}
}
