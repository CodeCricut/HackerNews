using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostCommentProcessor : IPostVerbProcessor<PostCommentModel, GetCommentModel>
	{

	}

	public class PostCommentProcessor : PostVerbProcessor<PostCommentModel, GetCommentModel>,
		IPostCommentProcessor
	{
		public PostCommentProcessor(ISignInManager signInManager, IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient, IEntityLogger<GetCommentModel> entityLogger) : base(signInManager, entityApiClient, entityLogger)
		{
		}

		protected override PostCommentModel ConstructPostModel(PostVerbOptions options)
		{
			return new PostCommentModel()
			{
				Text = options.CommentText,
				ParentArticleId = options.CommentArticleId,
				ParentCommentId = options.CommentCommentId
			};
		}
	}
}
