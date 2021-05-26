using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.MediatR.Commands.PostEntity;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.PostEntity
{
	public class PostCommentCommand : PostEntityCommand<PostCommentModel, GetCommentModel>
	{
		public PostCommentCommand(PostCommentModel postModel) : base(postModel)
		{
		}
	}

	public class PostCommentCommandHandler : PostEntityCommandHandler<PostCommentCommand, PostCommentModel, GetCommentModel>
	{
		public PostCommentCommandHandler(IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient) : base(entityApiClient)
		{
		}
	}
}
