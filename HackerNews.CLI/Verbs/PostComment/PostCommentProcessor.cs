using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Verbs.PostComment
{
	public interface IPostCommentProcessor : IPostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentOptions>
	{

	}

	public class PostCommentProcessor : PostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentOptions>,
		IPostCommentProcessor
	{
		public PostCommentProcessor(ISignInManager signInManager, IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient, IEntityLogger<GetCommentModel> entityLogger, ILogger<PostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentOptions>> logger, IVerbositySetter verbositySetter) : base(signInManager, entityApiClient, entityLogger, logger, verbositySetter)
		{
		}

		public override PostCommentModel ConstructPostModel(PostCommentOptions options)
		{
			return new PostCommentModel()
			{
				Text = options.Text,
				ParentArticleId = options.ArticleId,
				ParentCommentId = options.CommentId
			};
		}
	}
}
