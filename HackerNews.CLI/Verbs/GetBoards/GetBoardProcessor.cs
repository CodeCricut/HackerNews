﻿using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public interface IGetBoardProcessor : IGetVerbProcessor<GetBoardModel, GetBoardsVerbOptions>
	{

	}

	public class GetBoardProcessor : GetVerbProcessor<GetBoardModel, GetBoardsVerbOptions>, IGetBoardProcessor
	{
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _configEntityWriter;

		public GetBoardProcessor(IGetEntityRepository<GetBoardModel> entityRepository,
			IEntityLogger<GetBoardModel> entityLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetBoardsVerbOptions options)
		{
			_configEntityWriter.Configure(options.GetBoardInclusionConfiguration());
		}
	}
}