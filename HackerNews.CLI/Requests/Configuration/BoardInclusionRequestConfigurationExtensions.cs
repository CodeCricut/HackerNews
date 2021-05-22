using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.Configuration
{
	public static class BoardInclusionRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IBoardInclusionRequestConfiguration<TBaseRequestBuilder, TRequest> boardInclusionConfig,
			IBoardInclusionOptions opts,
			IIncludeAllOptions allIncludeOpts)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			BoardInclusionConfiguration inclusionConfig;
			if (allIncludeOpts.IncludeAllFields)
				inclusionConfig = new BoardInclusionConfiguration(true);
			else
				inclusionConfig = new BoardInclusionConfiguration()
				{
					IncludeArticleIds = opts.IncludeArticleIds,
					IncludeBoardImageId = opts.IncludeImageId,
					IncludeCreateDate = opts.IncludeCreateDate,
					IncludeCreatorId = opts.IncludeCreatorId,
					IncludeDeleted = opts.IncludeDeleted,
					IncludeDescription = opts.IncludeDescription,
					IncludeId = opts.IncludeId,
					IncludeModeratorIds = opts.IncludeModeratorIds,
					IncludeSubscriberIds = opts.IncludeSubscriberIds,
					IncludeTitle = opts.IncludeTitle
				};

			boardInclusionConfig.BoardInclusionConfiguration = inclusionConfig;

			return boardInclusionConfig.BaseRequest;
		}
	}
}
