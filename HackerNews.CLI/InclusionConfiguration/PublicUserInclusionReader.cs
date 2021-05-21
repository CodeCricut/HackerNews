using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.CLI.InclusionConfiguration
{
	public class PublicUserInclusionReader : 
		IEntityReader<GetPublicUserModel>,
		IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel>
	{
		public static readonly string ID = "ID";
		public static readonly string USERNAME = "USERNAME";
		public static readonly string KARMA = "KARMA";
		public static readonly string ARTICLE_IDS = "ARTICLE IDS";
		public static readonly string COMMENT_IDS = "COMMENT IDS";
		public static readonly string JOIN_DATE = "JOIN DATE";
		public static readonly string DELETED = "DELETED";
		public static readonly string PROFILE_IMAGE_ID = "PROFILE_IMAGE_ID";

		public IEnumerable<string> ReadAllKeys()
		{
			return new string[]
			{
				ID,
				USERNAME,
				KARMA,
				ARTICLE_IDS,
				COMMENT_IDS,
				JOIN_DATE,
				DELETED,
				PROFILE_IMAGE_ID
			};
		}

		public Dictionary<string, string> ReadAllKeyValues(GetPublicUserModel model)
		{
			string[] keys = ReadAllKeys().ToArray();
			string[] values = ReadAllValues(model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadAllValues(GetPublicUserModel user)
		{
			char delimiter = ',';

			var values = new List<string>();

			values.Add(user.Id.ToString()); ;
			values.Add(user.Username.Quote());
			values.Add(user.Karma.ToString());
			values.Add(user.ArticleIds.ToDelimitedList(delimiter).Quote());
			values.Add(user.CommentIds.ToDelimitedList(delimiter).Quote());
			values.Add(user.JoinDate.ToString().Quote());
			values.Add(user.Deleted.ToString());
			values.Add(user.ProfileImageId.ToString());

			return values;
		}

		public IEnumerable<string> ReadIncludedKeys(PublicUserInclusionConfiguration config)
		{
			var keys = new List<string>();

			if (config.IncludeId)
				keys.Add(ID);
			if (config.IncludeUsername)
				keys.Add(USERNAME);
			if (config.IncludeKarma)
				keys.Add(KARMA);
			if (config.IncludeArticleIds)
				keys.Add(ARTICLE_IDS);
			if (config.IncludeCommentIds)
				keys.Add(COMMENT_IDS);
			if (config.IncludeJoinDate)
				keys.Add(JOIN_DATE);
			if (config.IncludeDeleted)
				keys.Add(DELETED);
			if (config.IncludeProfileImageId)
				keys.Add(PROFILE_IMAGE_ID);

			return keys;
		}

		public Dictionary<string, string> ReadIncludedKeyValues(PublicUserInclusionConfiguration config, GetPublicUserModel model)
		{
			string[] keys = ReadIncludedKeys(config).ToArray();
			string[] values = ReadIncludedValues(config, model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadIncludedValues(PublicUserInclusionConfiguration config, GetPublicUserModel user)
		{
			char delimiter = ',';

			var values = new List<string>();

			if (config.IncludeId)
				values.Add(user.Id.ToString()); ;
			if (config.IncludeUsername)
				values.Add(user.Username.Quote());
			if (config.IncludeKarma)
				values.Add(user.Karma.ToString());
			if (config.IncludeArticleIds)
				values.Add(user.ArticleIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeCommentIds)
				values.Add(user.CommentIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeJoinDate)
				values.Add(user.JoinDate.ToString().Quote());
			if (config.IncludeDeleted)
				values.Add(user.Deleted.ToString());
			if (config.IncludeProfileImageId)
				values.Add(user.ProfileImageId.ToString());

			return values;
		}
	}
}
