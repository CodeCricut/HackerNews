using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.ApplicationRequests.Register
{
	public interface IRegisterRequestHandler
	{
		Task HandleAsync(RegisterRequest request);
	}

	public class RegisterRequestHandler : IRegisterRequestHandler
	{
		private readonly IMediator _mediator;

		public RegisterRequestHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task HandleAsync(RegisterRequest request)
		{
			await _mediator.Send(request.CreateVerbosityCommand());

			var jwt = await _mediator.Send(request.CreateRegisterCommand());

			await _mediator.Send(request.CreateLogJwtCommand(jwt));
		}
	}
}
