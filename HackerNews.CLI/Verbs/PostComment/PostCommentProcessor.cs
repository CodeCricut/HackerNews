using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Comments;

namespace HackerNews.CLI.Verbs.PostComment
{
	public interface IPostCommentProcessor : IPostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentVerbOptions>
	{

	}

	public class PostCommentProcessor : PostVerbProcessor<PostCommentModel, GetCommentModel, PostCommentVerbOptions>,
		IPostCommentProcessor
	{
		public PostCommentProcessor(ISignInManager signInManager, IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient, IEntityLogger<GetCommentModel> entityLogger) : base(signInManager, entityApiClient, entityLogger)
		{
		}

		public override PostCommentModel ConstructPostModel(PostCommentVerbOptions options)
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
