using HackerNews.CLI.ApplicationRequests.Register;
using HackerNews.CLI.Domain.Verbs;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class RegisterHostedService : IHostedService
	{
		private readonly RegisterOptions _options;
		private readonly IRegisterRequestHandler _requestHandler;

		public RegisterHostedService(RegisterOptions options,
			IRegisterRequestHandler requestHandler)
		{
			_options = options;
			_requestHandler = requestHandler;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var request = new RegisterRequest(_options);

			return _requestHandler.HandleAsync(request);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
