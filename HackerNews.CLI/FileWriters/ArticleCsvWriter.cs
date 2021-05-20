using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class ArticleCsvWriter : IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration>
	{
		private readonly IFileWriter _fileWriter;
		private readonly ILogger<ArticleCsvWriter> _logger;
		private readonly IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> _articleInclusionReader;
		private ArticleInclusionConfiguration _inclusionConfig;

		public ArticleCsvWriter(IFileWriter fileWriter,
			ILogger<ArticleCsvWriter> logger,
			IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel> articleInclusionReader)
		{
			_fileWriter = fileWriter;
			_logger = logger;
			_articleInclusionReader = articleInclusionReader;
			_inclusionConfig = new ArticleInclusionConfiguration();
		}

		public void Configure(ArticleInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public Task WriteEntityAsync(string fileLoc, GetArticleModel entity)
		{
			List<GetArticleModel> articles = new List<GetArticleModel>();
			articles.Add(entity);
			return WriteEntitiesToFileAsync(fileLoc, articles);
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetArticleModel> entityPage)
		{
			return WriteEntitiesToFileAsync(fileLoc, entityPage.Items);
		}

		private async Task WriteEntitiesToFileAsync(string fileLoc, IEnumerable<GetArticleModel> articles)
		{
			List<string> lines = new List<string>
			{
				GetHeadLine()
			};

			foreach (var article in articles)
				lines.Add(GetBodyLine(article));

			_logger.LogInformation($"Adding {lines.Count} lines to {fileLoc}...");

			try
			{
				await _fileWriter.WriteToFileAsync(fileLoc, lines);
				_logger.LogInformation($"Wrote {lines.Count} lines to {fileLoc}...");
			}
			catch (Exception)
			{
				_logger.LogError($"Error adding {lines.Count} lines to {fileLoc}. Aborting");
				throw;
			}
		}


		private string GetHeadLine()
		{
			var keys = _articleInclusionReader.ReadIncludedKeys(_inclusionConfig);

			StringBuilder sb = new StringBuilder();
			foreach (var key in keys)
			{
				sb.Append($"{key},");
			}

			return sb.ToString();
		}

		private string GetBodyLine(GetArticleModel article)
		{
			var values = _articleInclusionReader.ReadIncludedValues(_inclusionConfig, article);

			StringBuilder sb = new StringBuilder();
			foreach (var value in values)
			{
				sb.Append($"{value},");
			}

			return sb.ToString();
		}
	}
}
