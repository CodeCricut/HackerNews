using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
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
	}
}
