using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetBoardProcessor : IGetVerbProcessor<PostBoardModel, GetBoardModel>
	{

	}

	public class GetBoardProcessor : GetVerbProcessor<PostBoardModel, GetBoardModel>, IGetBoardProcessor
	{
		public GetBoardProcessor(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, IEntityLogger<GetBoardModel> entityLogger, IEntityWriter<GetBoardModel> entityWriter) : base(entityApiClient, entityLogger, entityWriter)
		{
		}

		protected override void ConfigureWriter(GetVerbOptions options, IEntityWriter<GetBoardModel> writer)
		{
			BoardInclusionConfiguration config = options.GetBoardInclusionConfiguration();
			writer.Configure(config);
		}
	}
}
