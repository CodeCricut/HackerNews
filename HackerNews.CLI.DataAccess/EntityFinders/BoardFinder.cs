using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.DataAccess.EntityFinders;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class BoardFinder : EntityFinder<GetBoardModel, PostBoardModel>
	{
		public BoardFinder(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, ILogger<EntityFinder<GetBoardModel, PostBoardModel>> logger) : base(entityApiClient, logger)
		{
		}
	}
}
