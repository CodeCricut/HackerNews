using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.ArticleServices
{
	public class ArticleApiReader : ApiReader<GetArticleModel>
	{
		public ArticleApiReader(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService)
		{
		}
	}
}
