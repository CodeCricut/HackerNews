using CommandLine;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Get
{
	[Verb("get", HelpText = "Get users.")]
	public class GetPublicUsersVerbOptions : GetEntityVerbOptions, IGetEntityVerbOptions
	{
		[Option('u', "users", Required = true, HelpText = "Get users.")]
		public bool GetUsers { get; set; }

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
