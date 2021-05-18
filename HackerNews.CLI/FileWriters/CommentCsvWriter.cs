using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class CommentCsvWriter : IEntityWriter<GetCommentModel>
	{
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
