using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Comments;

namespace HackerNews.ApiConsumer.EntityClients
{
	public interface ICommentApiClient : IEntityApiClient<PostCommentModel, GetCommentModel> { }

	internal class CommentApiClient : EntityApiClient<PostCommentModel, GetCommentModel>, ICommentApiClient
	{
		public CommentApiClient(IApiClient apiClient) : base(apiClient, "comments")
		{
		}
	}
}
