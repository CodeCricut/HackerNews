using HackerNews.ApiConsumer.Account;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Register
{
	public class RegisterHostedService : IHostedService
	{
		private readonly RegisterOptions _options;
		private readonly ILogger<RegisterHostedService> _logger;
		private readonly IRegistrationApiClient _registrationApiClient;
		private readonly IJwtLogger _jwtLogger;

		public RegisterHostedService(RegisterOptions options,
			ILogger<RegisterHostedService> logger,
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

			_logger.LogInformation($"Registering user...");

			Jwt jwt = await _registrationApiClient.RegisterAsync(registerModel);
			_jwtLogger.LogJwt(jwt);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
