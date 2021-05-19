using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class CommentCsvWriter : IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration>
	{
		public void Configure(CommentInclusionConfiguration config)
		{
			throw new NotImplementedException();
		}

		public Task WriteEntityAsync(string fileLoc, GetCommentModel entity)
		{
			throw new NotImplementedException();
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetCommentModel> entityPage)
		{
			throw new NotImplementedException();
		}
	}
}
