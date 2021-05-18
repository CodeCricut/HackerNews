using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
namespace HackerNews.CLI.Loggers
{
	public class CommentLogger : IEntityLogger<GetCommentModel>
	{
		private readonly ILogger<CommentLogger> _logger;

		public CommentLogger(ILogger<CommentLogger> logger)
		{
			_logger = logger;
		}

		public void LogEntity(GetCommentModel comment)
		{
			// TODO
			string printString = $"COMMENT {comment.Id}: Title={comment.Text}; PostDate{comment.PostDate}";
			_logger.LogInformation(printString);
		}

		public void LogEntityPage(PaginatedList<GetCommentModel> commentPage)
		{
			// TODO:
			_logger.LogInformation(commentPage.PageSize.ToString());
		}
	}
}
