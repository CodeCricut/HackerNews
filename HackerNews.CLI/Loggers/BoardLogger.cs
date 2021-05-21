using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.CLI.Loggers
{
	public class BoardLogger : EntityLogger<GetBoardModel>, IEntityLogger<GetBoardModel>
	{
		public BoardLogger(ILogger<EntityLogger<GetBoardModel>> logger, IEntityReader<GetBoardModel> entityReader) : base(logger, entityReader)
		{
		}

		protected override string GetEntityName()
			=> "Board";

		protected override string GetEntityPlural()
			=> "Boards";
	}
}
