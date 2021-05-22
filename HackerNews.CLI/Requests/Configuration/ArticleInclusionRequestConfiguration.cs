using HackerNews.CLI.InclusionConfiguration;
using System;

namespace HackerNews.CLI.Requests.Configuration
{
	public interface IArticleInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		ArticleInclusionConfiguration ArticleInclusionConfiguration { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<ArticleInclusionConfiguration> configCallback);
	}

	public class ArticleInclusionRequestConfiguration<TBaseRequestBuilder, TRequest> : IArticleInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public ArticleInclusionConfiguration ArticleInclusionConfiguration { get; set; }

		public ArticleInclusionRequestConfiguration(TBaseRequestBuilder baseRequestBuilder)
		{
			BaseRequest = baseRequestBuilder;

			ArticleInclusionConfiguration = new ArticleInclusionConfiguration();
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<ArticleInclusionConfiguration> configCallback)
		{
			BaseRequest.BuildActions.Add(() => ArticleInclusionConfiguration = configCallback());
			return BaseRequest;
		}
	}
}
