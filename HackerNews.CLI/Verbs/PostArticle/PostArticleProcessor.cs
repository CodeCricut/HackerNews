using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Verbs.PostArticle
{
	public interface IPostArticleProcessor : IPostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>
	{

	}

	public class PostArticleProcessor : PostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>, IPostArticleProcessor
	{
		public PostArticleProcessor(ISignInManager signInManager, IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient, IEntityLogger<GetArticleModel> entityLogger, ILogger<PostVerbProcessor<PostArticleModel, GetArticleModel, PostArticleOptions>> logger, IVerbositySetter verbositySetter) : base(signInManager, entityApiClient, entityLogger, logger, verbositySetter)
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
