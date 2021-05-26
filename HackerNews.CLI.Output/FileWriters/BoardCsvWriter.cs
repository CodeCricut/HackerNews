using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Output.FileWriters;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class BoardCsvWriter : EntityCsvWriter<GetBoardModel, BoardInclusionConfiguration>
	{
		public BoardCsvWriter(ICsvFileWriter csvWriter, ILogger<EntityCsvWriter<GetBoardModel, BoardInclusionConfiguration>> logger, IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel> inclusionReader, BoardInclusionConfiguration inclusionConfig) : base(csvWriter, logger, inclusionReader, inclusionConfig)
		{
		}
	}
}
