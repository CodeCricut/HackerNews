using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public interface IGetBoardProcessor : IGetVerbProcessor<PostBoardModel, GetBoardModel>
	{

	}

	public class GetBoardProcessor : GetVerbProcessor<PostBoardModel, GetBoardModel>, IGetBoardProcessor
	{
		public GetBoardProcessor(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, IEntityLogger<GetBoardModel> entityLogger) 
			: base(entityApiClient, entityLogger)
		{
		}
	}
}
