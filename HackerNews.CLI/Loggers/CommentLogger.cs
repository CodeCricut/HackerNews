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
			string printString = $"COMMENT {comment.Id}: Title={comment.Text}; PostDate{comment.PostDate}";
			_logger.LogInformation(printString);
		}

		public void LogEntityPage(PaginatedList<GetCommentModel> commentPage)
		{
			_logger.LogInformation($"Comment Page: PageSize{commentPage.PageSize}; {commentPage.PageIndex} / {commentPage.TotalCount}");
			_logger.LogInformation("Id\tText\tParentType\tParentId");

			foreach(var comment in commentPage.Items)
			{
				string parentType = comment.ParentArticleId <= 0
					? "Article"
					: "Comment";
				int parentId = comment.ParentArticleId <= 0
					? comment.ParentArticleId
					: comment.ParentCommentId;
				_logger.LogInformation($"{comment.Id}\t{comment.Text}\t{parentType}\t{parentId}");
			}
		}
	}
}
