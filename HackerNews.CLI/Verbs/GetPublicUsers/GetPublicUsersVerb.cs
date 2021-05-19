﻿using CommandLine;
using HackerNews.CLI.Verbs.GetEntity;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetPublicUsers
{
	[Verb("get-u", HelpText = "Get users from the database.")]
	public class GetPublicUsersVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		[Option("IncludeId")]
		public bool IncludeId { get; set; }
		[Option("IncludeUsername")]
		public bool IncludeUsername { get; set; }
		[Option("IncludeKarma")]
		public bool IncludeKarma { get; set; }
		[Option("IncludeArticleIds")]
		public bool IncludeArticleIds { get; set; }
		[Option("IncludeCommentIds")]
		public bool IncludeCommentIds { get; set; }
		[Option("includeUserJoinDate")]
		public bool IncludeJoinDate { get; set; }
		[Option("includeUserDeleted")]
		public bool IncludeDeleted { get; set; }
		[Option("includeUserProfileImageId")]
		public bool IncludeProfileImageId { get; set; }
	}

	public class GetPublicUsersVerb : IHostedService
	{
		private readonly GetPublicUsersVerbOptions _options;
		private readonly IGetPublicUserProcessor _getPublicUserProcessor;

		public GetPublicUsersVerb(GetPublicUsersVerbOptions options,
			IGetPublicUserProcessor getPublicUserProcessor)
		{
			_options = options;
			_getPublicUserProcessor = getPublicUserProcessor;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _getPublicUserProcessor.ProcessGetVerbOptionsAsync(_options);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
