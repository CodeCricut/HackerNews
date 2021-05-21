using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.CLI.InclusionConfiguration
{
	public class CommentInclusionReader : IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel>
	{
		public static readonly string ID = "ID";
		public static readonly string USER_ID = "USER ID";
		public static readonly string TEXT = "TEXT";
		public static readonly string URL = "URL";
		public static readonly string KARMA = "KARMA";
		public static readonly string COMMENT_IDS = "COMMENT IDS";
		public static readonly string PARENT_COMMENT_ID = "PARENT COMMENT ID";
		public static readonly string PARENT_ARTICLE_ID = "PARENT ARTICLE ID";
		public static readonly string DELETED = "DELETED";
		public static readonly string USERS_LIKED = "USERS LIKED";
		public static readonly string USERS_DISLIKED = "USERS DISLIKED";
		public static readonly string POST_DATE = "POST DATE";
		public static readonly string BOARD_ID = "BOARD ID";

		public IEnumerable<string> ReadAllKeys()
		{
			return new string[]
			{
				ID,
				USER_ID,
				TEXT,
				URL,
				KARMA,
				COMMENT_IDS,
				PARENT_COMMENT_ID,
				PARENT_ARTICLE_ID,
				DELETED,
				USERS_LIKED,
				USERS_DISLIKED,
				POST_DATE,
				BOARD_ID,
			};
		}

		public Dictionary<string, string> ReadAllKeyValues(GetCommentModel model)
		{
			string[] keys = ReadAllKeys().ToArray();
			string[] values = ReadAllValues(model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadAllValues(GetCommentModel comment)
		{
			char delimiter = ',';
			var values = new List<string>
			{
				comment.Id.ToString(),
				comment.UserId.ToString(),
				comment.Text.Quote(),
				comment.Url.Quote(),
				comment.Karma.ToString(),
				comment.CommentIds.ToDelimitedList(delimiter).Quote(),
				comment.ParentCommentId.ToString(),
				comment.ParentArticleId.ToString(),
				comment.Deleted.ToString(),
				comment.UsersLiked.ToDelimitedList(delimiter).Quote(),
				comment.UsersDisliked.ToDelimitedList(delimiter).Quote(),
				comment.PostDate.ToString().Quote(),
				comment.BoardId.ToString(),
			};

			return values;
		}

		public IEnumerable<string> ReadIncludedKeys(CommentInclusionConfiguration config)
		{
			var keys = new List<string>();

			if (config.IncludeId)
				keys.Add(ID);
			if (config.IncludeUserId)
				keys.Add(USER_ID);
			if (config.IncludeText)
				keys.Add(TEXT);
			if (config.IncludeUrl)
				keys.Add(URL);
			if (config.IncludeKarma)
				keys.Add(KARMA);
			if (config.IncludeCommentIds)
				keys.Add(COMMENT_IDS);
			if (config.IncludeParentCommentId)
				keys.Add(PARENT_COMMENT_ID);
			if (config.IncludeParentArticleId)
				keys.Add(PARENT_ARTICLE_ID);
			if (config.IncludeDeleted)
				keys.Add(DELETED);
			if (config.IncludeUsersLiked)
				keys.Add(USERS_LIKED);
			if (config.IncludeUsersDisliked)
				keys.Add(USERS_DISLIKED);
			if (config.IncludePostDate)
				keys.Add(POST_DATE);
			if (config.IncludeBoardId)
				keys.Add(BOARD_ID);

			return keys;
		}

		public Dictionary<string, string> ReadIncludedKeyValues(CommentInclusionConfiguration config, GetCommentModel model)
		{
			string[] keys = ReadIncludedKeys(config).ToArray();
			string[] values = ReadIncludedValues(config, model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadIncludedValues(CommentInclusionConfiguration config, GetCommentModel comment)
		{
			char delimiter = ',';
			var values = new List<string>();

			if (config.IncludeId)
				values.Add(comment.Id.ToString()); ;
			if (config.IncludeUserId)
				values.Add(comment.UserId.ToString());
			if (config.IncludeText)
				values.Add(comment.Text.Quote());
			if (config.IncludeUrl)
				values.Add(comment.Url.Quote());
			if (config.IncludeKarma)
				values.Add(comment.Karma.ToString());
			if (config.IncludeCommentIds)
				values.Add(comment.CommentIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeParentCommentId)
				values.Add(comment.ParentCommentId.ToString());
			if (config.IncludeParentArticleId)
				values.Add(comment.ParentArticleId.ToString());
			if (config.IncludeDeleted)
				values.Add(comment.Deleted.ToString());
			if (config.IncludeUsersLiked)
				values.Add(comment.UsersLiked.ToDelimitedList(delimiter).Quote());
			if (config.IncludeUsersDisliked)
				values.Add(comment.UsersDisliked.ToDelimitedList(delimiter).Quote());
			if (config.IncludePostDate)
				values.Add(comment.PostDate.ToString().Quote());
			if (config.IncludeBoardId)
				values.Add(comment.BoardId.ToString());

			return values;
		}
	}
}
