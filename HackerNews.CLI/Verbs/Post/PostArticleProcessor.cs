using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostArticleProcessor : IPostVerbProcessor<PostArticleModel, GetArticleModel>
	{

	}

	public class PostArticleProcessor : PostVerbProcessor<PostArticleModel, GetArticleModel>, IPostArticleProcessor
	{
		public PostArticleProcessor(ISignInManager signInManager, IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient, IEntityLogger<GetArticleModel> entityLogger) : base(signInManager, entityApiClient, entityLogger)
		{
		}

		protected override PostArticleModel ConstructPostModel(PostVerbOptions options)
		{
			return new PostArticleModel()
			{
				Title = options.ArticleTitle,
				Text = options.ArticleText,
				Type = options.ArticleType,
				BoardId = options.ArticleBoardId
			};
		}
	}
}
