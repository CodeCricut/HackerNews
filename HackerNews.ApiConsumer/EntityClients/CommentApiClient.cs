using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;

namespace HackerNews.ApiConsumer.EntityClients
{
	public interface ICommentApiClient : IEntityApiClient<PostCommentModel, GetCommentModel> { }

	internal class CommentApiClient : EntityApiClient<PostCommentModel, GetCommentModel>, ICommentApiClient
	{
		public CommentApiClient(IApiClient apiClient, ILogger<CommentApiClient> logger) : base(apiClient, logger, "comments")
		{
		}
	}
}
