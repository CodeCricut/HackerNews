using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Output.FileWriters
{
	public abstract class EntityCsvWriter<TGetModel, TInclusionConfig> : IConfigurableEntityWriter<TGetModel, TInclusionConfig>
		where TGetModel : GetModelDto
	{
		private readonly ICsvFileWriter _csvWriter;
		private readonly ILogger<EntityCsvWriter<TGetModel, TInclusionConfig>> _logger;
		private readonly IEntityInclusionReader<TInclusionConfig, TGetModel> _inclusionReader;
		
		private TInclusionConfig _inclusionConfig;

		public EntityCsvWriter(
			ICsvFileWriter csvWriter,
			ILogger<EntityCsvWriter<TGetModel, TInclusionConfig>> logger,
			IEntityInclusionReader<TInclusionConfig, TGetModel> inclusionReader,
			TInclusionConfig inclusionConfig)
		{
			_csvWriter = csvWriter;
			_logger = logger;
			_inclusionReader = inclusionReader;
			_inclusionConfig = inclusionConfig;
		}

		public void Configure(TInclusionConfig config)
		{
			_inclusionConfig = config;
		}

		public Task WriteEntityAsync(string fileLoc, TGetModel entity)
		{
			_logger.LogDebug($"Attempting to write entity to {fileLoc}...");

			CsvFile file = CsvFile.FromFileLocation(fileLoc);

			file.HeadLine = GetHeadLine();
			file.BodyLines.Add(GetBodyLine(entity));

			return _csvWriter.Write(file);
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<TGetModel> entityPage)
		{
			_logger.LogDebug($"Attempting to write page with {entityPage.Items.Count} entities to {fileLoc}...");

			CsvFile file = CsvFile.FromFileLocation(fileLoc);
			file.HeadLine = GetHeadLine();

			foreach (var entity in entityPage.Items)
				file.BodyLines.Add(GetBodyLine(entity));

			return _csvWriter.Write(file);
		}

		private CsvLine GetHeadLine()
		{
			var keys = _inclusionReader.ReadIncludedKeys(_inclusionConfig);

			CsvLine head = CsvLine.FromLines(keys);

			return head;
		}

		private CsvLine GetBodyLine(TGetModel entity)
		{
			var values = _inclusionReader.ReadIncludedValues(_inclusionConfig, entity);

			CsvLine line = CsvLine.FromLines(values);

			return line;
		}
	}
}
