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
		private ArticleInclusionConfiguration _inclusionConfig;

		public ArticleCsvWriter(IFileWriter fileWriter,
			ILogger<ArticleCsvWriter> logger)
		{
			_fileWriter = fileWriter;
			_logger = logger;
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
			StringBuilder head = new StringBuilder();
			if (_inclusionConfig.IncludeId) head.Append("ID,");
			if (_inclusionConfig.IncludeType) head.Append("TYPE,");
			if (_inclusionConfig.IncludeUserId) head.Append("USER ID,");
			if (_inclusionConfig.IncludeText) head.Append("TEXT,");
			if (_inclusionConfig.IncludeCommentIds) head.Append("COMMENT IDS,");
			if (_inclusionConfig.IncludeKarma) head.Append("KARMA");
			if (_inclusionConfig.IncludeTitle) head.Append("TITLE");
			if (_inclusionConfig.IncludeUsersLiked) head.Append("USERS LIKED");
			if (_inclusionConfig.IncludeUsersDisliked) head.Append("USERS DISLIKED");
			if (_inclusionConfig.IncludePostDate) head.Append("POST DATE");
			if (_inclusionConfig.IncludeBoardId) head.Append("BOARD ID");
			if (_inclusionConfig.IncludeDeleted) head.Append("DELETED");
			if (_inclusionConfig.IncludeAssociatedImageId) head.Append("ASSOCIATED IMAGE ID");

			return head.ToString();
		}

		private string GetBodyLine(GetArticleModel article)
		{
			char delimiter = ',';

			StringBuilder body = new StringBuilder();
			if (_inclusionConfig.IncludeId)
				body.Append($"{article.Id},");
			if (_inclusionConfig.IncludeType)
				body.Append($"{article.Type.ToString().Quote()},");
			if (_inclusionConfig.IncludeUserId)
				body.Append($"{article.UserId},");
			if (_inclusionConfig.IncludeText)
				body.Append($"{article.Text.Quote()},");
			if (_inclusionConfig.IncludeCommentIds)
				body.Append($"{article.CommentIds.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludeKarma)
				body.Append($"{article.Karma},");
			if (_inclusionConfig.IncludeTitle)
				body.Append($"{article.Title.Quote()},");
			if (_inclusionConfig.IncludeUsersLiked)
				body.Append($"{article.UsersLiked.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludeUsersDisliked)
				body.Append($"{article.UsersDisliked.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludePostDate)
				body.Append($"{article.PostDate.ToString().Quote()},");
			if (_inclusionConfig.IncludeBoardId)
				body.Append($"{article.BoardId},");
			if (_inclusionConfig.IncludeDeleted)
				body.Append($"{article.Deleted},");
			if (_inclusionConfig.IncludeAssociatedImageId)
				body.Append($"{article.AssociatedImageId},");

			return body.ToString();
		}
	}
}
