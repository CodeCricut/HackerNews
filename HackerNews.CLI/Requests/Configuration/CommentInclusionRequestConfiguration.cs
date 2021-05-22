using HackerNews.CLI.InclusionConfiguration;
using System;

namespace HackerNews.CLI.Requests.Configuration
{
	public interface ICommentInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		CommentInclusionConfiguration CommentInclusionConfiguration { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<CommentInclusionConfiguration> configCallback);
	}

	public class CommentInclusionRequestConfiguration<TBaseRequestBuilder, TRequest> : ICommentInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public CommentInclusionConfiguration CommentInclusionConfiguration { get; set; }

		public CommentInclusionRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;

			CommentInclusionConfiguration = new CommentInclusionConfiguration();
		}

		// TODO: could this be an extension method?
		public TBaseRequestBuilder SetWhenBuilt(Func<CommentInclusionConfiguration> configCallback)
		{
			BaseRequest.BuildActions.Add(() => CommentInclusionConfiguration = configCallback());
			return BaseRequest;
		}
	}
}
