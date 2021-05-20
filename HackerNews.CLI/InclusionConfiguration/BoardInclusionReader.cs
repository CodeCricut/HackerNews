using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.CLI.InclusionConfiguration
{
	public class BoardInclusionReader : IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel>
	{
		private static readonly string ID = "ID";
		private static readonly string TITLE = "TITLE";
		private static readonly string DESCRIPTION = "Description";
		private static readonly string CREATE_DATE = "CREATE DATE";
		private static readonly string CREATOR_ID = "CREATOR ID";
		private static readonly string MODERATOR_IDS = "MODERATOR IDS";
		private static readonly string SUBSCRIBER_IDS = "SUBSCRIBER IDS";
		private static readonly string ARTICLE_IDS = "ARTICLE IDS";
		private static readonly string DELETED = "DELETED";
		private static readonly string BOARD_IMAGE_ID = "BOARD IMAGE ID";

		public IEnumerable<string> ReadAllKeys(BoardInclusionConfiguration config)
		{
			return new string[]
			{
				ID,
				TITLE,
				DESCRIPTION,
				CREATE_DATE,
				CREATOR_ID,
				MODERATOR_IDS,
				SUBSCRIBER_IDS,
				ARTICLE_IDS,
				DELETED,
				BOARD_IMAGE_ID
			};
		}

		public Dictionary<string, string> ReadAllKeyValues(BoardInclusionConfiguration config, GetBoardModel model)
		{
			string[] keys = ReadAllKeys(config).ToArray();
			string[] values = ReadAllValues(config, model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}

		public IEnumerable<string> ReadAllValues(BoardInclusionConfiguration config, GetBoardModel board)
		{
			var values = new List<string>();
			char delimiter = ',';
			
			values.Add(board.Id.ToString());
			values.Add(board.Title.Quote());
			values.Add(board.Description.Quote());
			values.Add(board.CreateDate.ToString().Quote());
			values.Add(board.CreatorId.ToString());
			values.Add(board.ModeratorIds.ToDelimitedList(delimiter).Quote());
			values.Add(board.SubscriberIds.ToDelimitedList(delimiter).Quote());
			values.Add(board.ArticleIds.ToDelimitedList(delimiter).Quote());
			values.Add(board.Deleted.ToString());
			values.Add(board.BoardImageId.ToString());

			return values;
		}

		public IEnumerable<string> ReadIncludedKeys(BoardInclusionConfiguration config)
		{
			var keys = new List<string>();
			if (config.IncludeId) 
				keys.Add(ID);
			if (config.IncludeTitle) 
				keys.Add(TITLE);
			if (config.IncludeDescription) 
				keys.Add(DESCRIPTION);
			if (config.IncludeCreateDate) 
				keys.Add(CREATE_DATE);
			if (config.IncludeCreatorId) 
				keys.Add(CREATOR_ID);
			if (config.IncludeModeratorIds) 
				keys.Add(MODERATOR_IDS);
			if (config.IncludeSubscriberIds) 
				keys.Add(SUBSCRIBER_IDS);
			if (config.IncludeArticleIds) 
				keys.Add(ARTICLE_IDS);
			if (config.IncludeDeleted) 
				keys.Add(DELETED);
			if (config.IncludeBoardImageId) 
				keys.Add(BOARD_IMAGE_ID);

			return keys;
		}

		public Dictionary<string, string> ReadIncludedKeyValues(BoardInclusionConfiguration config, GetBoardModel model)
		{
			string[] keys = ReadIncludedKeys(config).ToArray();
			string[] values = ReadIncludedValues(config, model).ToArray();

			return DictionaryUtil.KvpToDictionary(keys, values);
		}
		

		public IEnumerable<string> ReadIncludedValues(BoardInclusionConfiguration config, GetBoardModel board)
		{
			var values = new List<string>();
			char delimiter = ',';


			if (config.IncludeId) 
				values.Add(board.Id.ToString());
			if (config.IncludeTitle) 
				values.Add(board.Title.Quote());
			if (config.IncludeDescription) 
				values.Add(board.Description.Quote());
			if (config.IncludeCreateDate) 
				values.Add(board.CreateDate.ToString().Quote());
			if (config.IncludeCreatorId) 
				values.Add(board.CreatorId.ToString());
			if (config.IncludeModeratorIds) 
				values.Add(board.ModeratorIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeSubscriberIds) 
				values.Add(board.SubscriberIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeArticleIds) 
				values.Add(board.ArticleIds.ToDelimitedList(delimiter).Quote());
			if (config.IncludeDeleted) 
				values.Add(board.Deleted.ToString());
			if (config.IncludeBoardImageId) 
				values.Add(board.BoardImageId.ToString());

			return values;
		}
	}
}
