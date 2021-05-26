using HackerNews.CLI.FileWriters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Output.FileWriters
{
	public interface ICsvFileWriter
	{
		Task Write(CsvFile file);
	}

	public class CsvFileWriter : ICsvFileWriter
	{
		private readonly ILogger<CsvFileWriter> _logger;
		private readonly IFileWriter _fileWriter;

		public CsvFileWriter(ILogger<CsvFileWriter> logger, 
			IFileWriter fileWriter)
		{
			_logger = logger;
			_fileWriter = fileWriter;
		}

		public async Task Write(CsvFile file)
		{
			_logger.LogInformation($"Adding {file.NumLines} lines to {file.FileLocation}...");

			try
			{
				var lineStrings = file.AllLines.Select(line => line.ToDelimitedList(','));
				await _fileWriter.WriteToFileAsync(file.FileLocation, lineStrings);
				_logger.LogInformation($"Wrote {file.NumLines} lines to {file.FileLocation}...");
			}
			catch (Exception)
			{
				_logger.LogError($"Error adding {file.NumLines} lines to {file.FileLocation}. Aborting");
			}
		}
	}
}
