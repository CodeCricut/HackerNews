using CommandLine;
using HackerNews.ApiConsumer.Account;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Register
{
	[Verb("register", HelpText = "Register as a new user, and print the returned JWT if successful.")]
	public class RegisterVerbOptions
	{
		[Option('u', "username", Required = true, HelpText = "The username to register with.")]
		public string Username { get; set; }

		[Option('p', "password", Required = true, HelpText = "The password to register with.")]
		public string Password { get; set; }

		[Option('f', "firstname", Required = true, HelpText = "The first name to register with.")]
		public string Firstname { get; set; }

		[Option('l', "lastname", Required = true, HelpText = "The last name to register with.")]
		public string Lastname { get; set; }
	}

	public class RegisterVerb : IHostedService
	{
		private readonly RegisterVerbOptions _options;
		private readonly ILogger<RegisterVerb> _logger;
		private readonly IRegistrationApiClient _registrationApiClient;
		private readonly IJwtLogger _jwtLogger;

		public RegisterVerb(RegisterVerbOptions options,
			ILogger<RegisterVerb> logger,
			IRegistrationApiClient registrationApiClient,
			IJwtLogger jwtLogger
			)
		{
			_options = options;
			_logger = logger;
			_registrationApiClient = registrationApiClient;
			_jwtLogger = jwtLogger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			RegisterUserModel registerModel = new RegisterUserModel()
			{
				UserName = _options.Username,
				Password = _options.Password,
				FirstName = _options.Firstname,
				LastName = _options.Lastname
			};
			Jwt jwt = await _registrationApiClient.RegisterAsync(registerModel);
			_jwtLogger.LogJwt(jwt);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
