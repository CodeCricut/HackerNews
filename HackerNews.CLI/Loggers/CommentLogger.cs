using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Loggers
{
	public class CommentLogger : EntityLogger<GetCommentModel>, IEntityLogger<GetCommentModel>
	{
		public CommentLogger(ILogger<EntityLogger<GetCommentModel>> logger, IEntityReader<GetCommentModel> entityReader) : base(logger, entityReader)
		{
		}

		protected override string GetEntityName()
			=> "Comment";

		protected override string GetEntityPlural()
			=> "Comments";
	}
}
