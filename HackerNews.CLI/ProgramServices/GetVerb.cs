using CommandLine;
using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.EntityClients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.ProgramServices
{
	[Verb("get", HelpText = "Retrieve data from the server, typically with the GET http verb.")]
	public class GetVerbOptions
	{
		[Option('t', "type", Required = true, HelpText = "The type of entity to retrieve (board, article, comment, user).")]
		public string Type { get; set; }

		[Option("id", HelpText = "The ID of the entity to be gotten.")]
		public int Id { get; set; }

		[Option('n', "page-number", HelpText = "The page number of entities to retrievw.")]
		public int PageNumber { get; set; }

		[Option('s', "page-size", HelpText = "The page size of entities to retrieve.")]
		public int PageSize { get; set; }

		[Option("ids", HelpText = "The IDs of the entity to be gotten.")]
		public IEnumerable<int> Ids { get; set; }
	}

	public class GetVerb : BaseVerb
	{
		private readonly GetVerbOptions _opts;
		private readonly ILogger<GetVerb> _logger;
		private readonly GetVerbOptions _options;

		public GetVerb(
			ILogger<GetVerb> logger,
			GetVerbOptions options,

			IBoardApiClient boardApiClient, 
			IArticleApiClient articleApiClient, 
			ICommentApiClient commentApiClient, 
			IUserApiClient userApiClient, 
			IRegistrationApiClient registrationApiClient, 
			IPrivateUserApiClient privateUserApiClient) 
			: base(boardApiClient, articleApiClient, commentApiClient, userApiClient, registrationApiClient, privateUserApiClient)
		{
			_logger = logger;
			_options = options;
		}

		public override Task StartAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
