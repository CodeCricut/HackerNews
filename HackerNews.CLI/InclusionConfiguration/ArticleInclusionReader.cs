using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.CLI.InclusionConfiguration
{
	public class ArticleInclusionReader :
		IEntityReader<GetArticleModel>,
		IEntityInclusionReader<ArticleInclusionConfiguration, GetArticleModel>
	{
		public ArticleInclusionReader(ILogger<ArticleInclusionReader> logger)
		{
			_logger = logger;

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		private static readonly string ID = "ID";
		private static readonly string TYPE = "TYPE";
		private static readonly string USER_ID = "USER ID";
		private static readonly string TEXT = "TEXT";
		private static readonly string COMMENT_IDS = "COMMENT IDS";
		private static readonly string KARMA = "KARMA";
		private static readonly string TITLE = "TITLE";
		private static readonly string USERS_LIKED = "USERS LIKED";
		private static readonly string USERS_DISLIKED = "USERS DISLIKED";
		private static readonly string POST_DATE = "POST DATE";
		private static readonly string BOARD_ID = "BOARD ID";
		private static readonly string DELETED = "DELETED";
		private static readonly string ASSOCIATED_IMAGE_ID = "ASSOCIATED IMAGE ID";
		private readonly ILogger<ArticleInclusionReader> _logger;

		public IEnumerable<string> ReadAllKeys()
		{
			_logger.LogTrace("Reading all keys of article.");

			return new string[]
			{
				ID,
				TYPE,
				USER_ID,
				TEXT,
				COMMENT_IDS,
				KARMA,
				TITLE,
				USERS_LIKED,
				USERS_DISLIKED,
				POST_DATE,
				BOARD_ID,
				DELETED,
				ASSOCIATED_IMAGE_ID
			};
		}

		public Dictionary<string, string> ReadAllKeyValues(GetArticleModel model)
		{
			_logger.LogTrace("Reading all key-value-pairs of article.");

			string[] keys = ReadAllKeys().ToArray();
			string[] values = ReadAllValues(model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadAllValues(GetArticleModel article)
		{
			_logger.LogTrace("Reading all values of article.");

			var values = new List<string>();
			char delimiter = ',';

			values.Add(article.Id.ToString());
			values.Add(article.Type.ToString().Quote());
			values.Add(article.UserId.ToString());
			values.Add(article.Text.Quote());
			values.Add(article.CommentIds.ToDelimitedList(delimiter).Quote());
			values.Add(article.Karma.ToString());
			values.Add(article.Title.Quote());
			values.Add(article.UsersLiked.ToDelimitedList(delimiter).Quote());
			values.Add(article.UsersDisliked.ToDelimitedList(delimiter).Quote());
			values.Add(article.PostDate.ToString().Quote());
			values.Add(article.BoardId.ToString());
			values.Add(article.Deleted.ToString());
			values.Add(article.AssociatedImageId.ToString());

			return values;
		}

		public IEnumerable<string> ReadIncludedKeys(ArticleInclusionConfiguration config)
		{
			_logger.LogTrace("Reading included keys of article.");

			var keys = new List<string>();
			if (config.IncludeId)
				keys.Add(ID);
			if (config.IncludeType)
				keys.Add(TYPE);
			if (config.IncludeUserId)
				keys.Add(USER_ID);
			if (config.IncludeText)
				keys.Add(TEXT);
			if (config.IncludeCommentIds)
				keys.Add(COMMENT_IDS);
			if (config.IncludeKarma)
				keys.Add(KARMA);
			if (config.IncludeTitle)
				keys.Add(TITLE);
			if (config.IncludeUsersLiked)
				keys.Add(USERS_LIKED);
			if (config.IncludeUsersDisliked)
				keys.Add(USERS_DISLIKED);
			if (config.IncludePostDate)
				keys.Add(POST_DATE);
			if (config.IncludeBoardId)
				keys.Add(BOARD_ID);
			if (config.IncludeDeleted)
				keys.Add(DELETED);
			if (config.IncludeAssociatedImageId)
				keys.Add(ASSOCIATED_IMAGE_ID);

			return keys;
		}

		public Dictionary<string, string> ReadIncludedKeyValues(ArticleInclusionConfiguration config, GetArticleModel model)
		{
			_logger.LogTrace("Reading included key-value-pairs of article.");

			string[] keys = ReadIncludedKeys(config).ToArray();
			string[] values = ReadIncludedValues(config, model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadIncludedValues(ArticleInclusionConfiguration config, GetArticleModel article)
		{
			_logger.LogTrace("Reading included values of article.");

			var values = new List<string>();
			char delimiter = ',';

			if (config.IncludeId)
				values.Add(article.Id.ToString());
			if (config.IncludeType)
				values.Add(article.Type.ToString().Quote());
			if (config.IncludeUserId)
				values.Add(article.UserId.ToString());
			if (config.IncludeText)
				values.Add(article.Text.Quote());
			if (config.IncludeCommentIds)
				values.Add(article.CommentIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeKarma)
				values.Add(article.Karma.ToString());
			if (config.IncludeTitle)
				values.Add(article.Title.Quote());
			if (config.IncludeUsersLiked)
				values.Add(article.UsersLiked.ToDelimitedList(delimiter).Quote());
			if (config.IncludeUsersDisliked)
				values.Add(article.UsersDisliked.ToDelimitedList(delimiter).Quote());
			if (config.IncludePostDate)
				values.Add(article.PostDate.ToString().Quote());
			if (config.IncludeBoardId)
				values.Add(article.BoardId.ToString());
			if (config.IncludeDeleted)
				values.Add(article.Deleted.ToString());
			if (config.IncludeAssociatedImageId)
				values.Add(article.AssociatedImageId.ToString());

			return values;
		}
	}
}
