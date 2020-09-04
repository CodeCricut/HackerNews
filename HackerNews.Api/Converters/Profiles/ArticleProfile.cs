using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters.Profiles
{
	public class ArticleProfile : Profile
	{
		public ArticleProfile()
		{
			CreateMap<Article, PostArticleModel>();
			CreateMap<PostArticleModel, Article>();

			CreateMap<Article, GetArticleModel>();
			CreateMap<GetArticleModel, Article>();
		}
	}
}
