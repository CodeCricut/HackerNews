using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters
{
	public class ArticleConverter : EntityConverter<Article, PostArticleModel, GetArticleModel>
	{
		public ArticleConverter(IMapper mapper) : base(mapper)
		{
		}

		public override async Task<DestinationT> ConvertEntityAsync<DestinationT>(Article entity)
		{
			return await Task.Factory.StartNew(() => _mapper.Map<DestinationT>(entity));
		}

		public override async Task<Article> ConvertEntityModelAsync(PostArticleModel entityModel)
		{
			return await Task.Factory.StartNew(() => _mapper.Map<Article>(entityModel));
		}
	}
}
