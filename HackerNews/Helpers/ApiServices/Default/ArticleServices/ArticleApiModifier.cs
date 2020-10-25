using HackerNews.Domain.Entities;
using HackerNews.Domain.Models.Articles;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers.ApiServices.Default.ArticleServices
{
	public class ArticleApiModifier : ApiModifier<Article, PostArticleModel, GetArticleModel>
	{
		public ArticleApiModifier(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService)
		{
		}
	}
}
