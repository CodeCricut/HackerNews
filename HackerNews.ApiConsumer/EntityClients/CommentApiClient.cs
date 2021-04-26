using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

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
