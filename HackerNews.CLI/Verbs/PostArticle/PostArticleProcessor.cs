using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Articles;

namespace HackerNews.CLI.Verbs.PostArticle
{
	public interface IPostArticleProcessor : IPostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>
	{

	}

	public class PostArticleProcessor : PostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>, IPostArticleProcessor
	{
		public PostArticleProcessor(ISignInManager signInManager,
			IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient,
			IEntityLogger<GetArticleModel> entityLogger)
			: base(signInManager, entityApiClient, entityLogger)
		{
		}

		public override PostArticleModel ConstructPostModel(PostArticleOptions options)
		{
			return new PostArticleModel()
			{
				Title = options.Title,
				Text = options.Text,
				Type = options.Type,
				BoardId = options.BoardId
			};
		}
	}
}
