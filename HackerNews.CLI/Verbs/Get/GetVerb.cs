﻿using CommandLine;
using HackerNews.CLI.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	interface IGetBoardVerbOptions
	{
		[Option("includeBoardId", SetName = "boards")]
		public bool IncludeBoardId { get; set; }
		[Option("includeBoardTitle", SetName = "boards")]
		public bool IncludeBoardTitle { get; set; }
		[Option("includeBoardDescription", SetName = "boards")]
		public bool IncludeBoardDescription { get; set; }
		[Option("includeBoardCreateDate", SetName = "boards")]
		public bool IncludeBoardCreateDate { get; set; }
		[Option("includeBoardCreatorId", SetName = "boards")]
		public bool IncludeBoardCreatorId { get; set; }
		[Option("includeBoardModeratorIds", SetName = "boards")]
		public bool IncludeBoardModeratorIds { get; set; }
		[Option("includeBoardSubscriberIds", SetName = "boards")]
		public bool IncludeBoardSubscriberIds { get; set; }
		[Option("includeBoardArticleIds", SetName = "boards")]
		public bool IncludeBoardArticleIds { get; set; }
		[Option("includeBoardDeleted", SetName = "boards")]
		public bool IncludeBoardDeleted { get; set; }
		[Option("includeBoardImageId", SetName = "boards")]
		public bool IncludeBoardImageId { get; set; }
	}

	interface IGetArticleVerbOptions
	{
		[Option("includeArticleId", SetName = "articles")]
		public bool IncludeArticleId { get; set; }
		[Option("includeArticleType", SetName = "articles")]
		public bool IncludeArticleType { get; set; }
		[Option("includeArticleUserId", SetName = "articles")]
		public bool IncludeArticleUserId { get; set; }
		[Option("includeArticleText", SetName = "articles")]
		public bool IncludeArticleText { get; set; }
		[Option("includeArticleCommentIds", SetName = "articles")]
		public bool IncludeArticleCommentIds { get; set; }
		[Option("includeArticleKarma", SetName = "articles")]
		public bool IncludeArticleKarma { get; set; }
		[Option("includeArticleTitle", SetName = "articles")]
		public bool IncludeArticleTitle { get; set; }
		[Option("includeArticleUsersLiked", SetName = "articles")]
		public bool IncludeArticleUsersLiked { get; set; }
		[Option("includeArticleUsersDisliked", SetName = "articles")]
		public bool IncludeArticleUsersDisliked { get; set; }
		[Option("includeArticlePostDate", SetName = "articles")]
		public bool IncludeArticlePostDate { get; set; }
		[Option("includeArticleBoardId", SetName = "articles")]
		public bool IncludeArticleBoardId { get; set; }
		[Option("includeArticleDeleted", SetName = "articles")]
		public bool IncludeArticleDeleted { get; set; }
		[Option("includeArticleAssociatedImageId", SetName = "articles")]
		public bool IncludeArticleAssociatedImageId { get; set; }
	}

	interface IGetCommentVerbOptions
	{
		[Option("includeCommentId", SetName = "comments")]
		public bool IncludeCommentId { get; set; }
		[Option("includeCommentUserId", SetName = "comments")]
		public bool IncludeCommentUserId { get; set; }
		[Option("includeCommentText", SetName = "comments")]
		public bool IncludeCommentText { get; set; }
		[Option("includeCommentUrl", SetName = "comments")]
		public bool IncludeCommentUrl { get; set; }
		[Option("includeCommentKarma", SetName = "comments")]
		public bool IncludeCommentKarma { get; set; }
		[Option("includeCommentCommentIds", SetName = "comments")]
		public bool IncludeCommentCommentIds { get; set; }
		[Option("includeCommentParentCommentId", SetName = "comments")]
		public bool IncludeCommentParentCommentId { get; set; }
		[Option("includeCommentParentArticleId", SetName = "comments")]
		public bool IncludeCommentParentArticleId { get; set; }
		[Option("includeCommentDeleted", SetName = "comments")]
		public bool IncludeCommentDeleted { get; set; }
		[Option("includeCommentUsersLiked", SetName = "comments")]
		public bool IncludeCommentUsersLiked { get; set; }
		[Option("includeCommentUsersDisliked", SetName = "comments")]
		public bool IncludeCommentUsersDisliked { get; set; }
		[Option("includeCommentPostDate", SetName = "comments")]
		public bool IncludeCommentPostDate { get; set; }
		[Option("includeCommentBoardId", SetName = "comments")]
		public bool IncludeCommentBoardId { get; set; }
	}

	interface IGetPublicUserVerbOptions
	{
		[Option("includeUserId", SetName = "users")]
		public bool IncludeUserId { get; set; }
		[Option("includeUserUsername", SetName = "users")]
		public bool IncludeUserUsername { get; set; }
		[Option("includeUserKarma", SetName = "users")]
		public bool IncludeUserKarma { get; set; }
		[Option("includeUserArticleIds", SetName = "users")]
		public bool IncludeUserArticleIds { get; set; }
		[Option("includeUserCommentIds", SetName = "users")]
		public bool IncludeUserCommentIds { get; set; }
		[Option("includeUserJoinDate", SetName = "users")]
		public bool IncludeUserJoinDate { get; set; }
		[Option("includeUserDeleted", SetName = "users")]
		public bool IncludeUserDeleted { get; set; }
		[Option("includeUserProfileImageId", SetName = "users")]
		public bool IncludeUserProfileImageId { get; set; }
	}

	[Verb("get", HelpText = "Retrieve data from the server, typically with the GET http verb.")]
	public class GetVerbOptions : IGetBoardVerbOptions, IGetArticleVerbOptions, IGetCommentVerbOptions, IGetPublicUserVerbOptions
	{
		[Option('t', "type", Required = true, HelpText = "The type of entity to retrieve (board, article, comment, user).")]
		public string Type { get; set; }

		[Option('p', "print", HelpText = "Print the entities to the console")]
		public bool Print { get; set; }

		[Option('f', "file",  HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string File { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }

		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }

		[Option("includeAllFields", HelpText = "Output all fields of the retrieved boards")]
		public bool IncludeAllFields { get; set; }

		// Boards
		public bool IncludeBoardId { get; set; }
		public bool IncludeBoardTitle { get; set; }
		public bool IncludeBoardDescription { get; set; }
		public bool IncludeBoardCreateDate { get; set; }
		public bool IncludeBoardCreatorId { get; set; }
		public bool IncludeBoardModeratorIds { get; set; }
		public bool IncludeBoardSubscriberIds { get; set; }
		public bool IncludeBoardArticleIds { get; set; }
		public bool IncludeBoardDeleted { get; set; }
		public bool IncludeBoardImageId { get; set; }

		// Articles
		public bool IncludeArticleId { get; set; }
		public bool IncludeArticleType { get; set; }
		public bool IncludeArticleUserId { get; set; }
		public bool IncludeArticleText { get; set; }
		public bool IncludeArticleCommentIds { get; set; }
		public bool IncludeArticleKarma { get; set; }
		public bool IncludeArticleTitle { get; set; }
		public bool IncludeArticleUsersLiked { get; set; }
		public bool IncludeArticleUsersDisliked { get; set; }
		public bool IncludeArticlePostDate { get; set; }
		public bool IncludeArticleBoardId { get; set; }
		public bool IncludeArticleDeleted { get; set; }
		public bool IncludeArticleAssociatedImageId { get; set; }

		// Comments
		public bool IncludeCommentId { get; set; }
		public bool IncludeCommentUserId { get; set; }
		public bool IncludeCommentText { get; set; }
		public bool IncludeCommentUrl { get; set; }
		public bool IncludeCommentKarma { get; set; }
		public bool IncludeCommentCommentIds { get; set; }
		public bool IncludeCommentParentCommentId { get; set; }
		public bool IncludeCommentParentArticleId { get; set; }
		public bool IncludeCommentDeleted { get; set; }
		public bool IncludeCommentUsersLiked { get; set; }
		public bool IncludeCommentUsersDisliked { get; set; }
		public bool IncludeCommentPostDate { get; set; }
		public bool IncludeCommentBoardId { get; set; }

		//  Users
		public bool IncludeUserId { get; set; }
		public bool IncludeUserUsername { get; set; }
		public bool IncludeUserKarma { get; set; }
		public bool IncludeUserArticleIds { get; set; }
		public bool IncludeUserCommentIds { get; set; }
		public bool IncludeUserJoinDate { get; set; }
		public bool IncludeUserDeleted { get; set; }
		public bool IncludeUserProfileImageId { get; set; }
	}

	public class GetVerb : IHostedService
	{
		private readonly ILogger<GetVerb> _logger;
		private readonly GetVerbOptions _options;
		private readonly IGetBoardProcessor _boardVerbProcessor;
		private readonly IGetArticleProcessor _articleVerbProcessor;
		private readonly IGetCommentProcessor _commentVerbProcessor;
		private readonly IGetPublicUserProcessor _userVerbProcessor;

		public GetVerb(
			GetVerbOptions options,
			ILogger<GetVerb> logger,
			IGetBoardProcessor boardVerbProcessor,
			IGetArticleProcessor articleVerbProcessor,
			IGetCommentProcessor commentVerbProcessor,
			IGetPublicUserProcessor userVerbProcessor)
		{
			_options = options;
			_logger = logger;
			_boardVerbProcessor = boardVerbProcessor;
			_articleVerbProcessor = articleVerbProcessor;
			_commentVerbProcessor = commentVerbProcessor;
			_userVerbProcessor = userVerbProcessor;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			if (_options.Type.IsBoardType())
				await _boardVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsArticleType())
				await _articleVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsCommentType())
				await _commentVerbProcessor.ProcessGetVerbOptionsAsync(_options);
			else if (_options.Type.IsPublicUserType())
				await _userVerbProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
