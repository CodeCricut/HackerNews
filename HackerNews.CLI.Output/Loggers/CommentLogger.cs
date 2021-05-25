using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;

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
