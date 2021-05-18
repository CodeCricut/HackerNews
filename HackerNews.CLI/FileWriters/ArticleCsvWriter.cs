using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class ArticleCsvWriter : IEntityWriter<GetArticleModel>
	{
		public Task WriteEntityAsync(string fileLoc, GetArticleModel entity)
		{
			throw new NotImplementedException();
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetArticleModel> entityPage)
		{
			throw new NotImplementedException();
		}
	}
}
