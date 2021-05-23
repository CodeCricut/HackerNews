using HackerNews.CLI.Request.Core;
using HackerNews.CLI.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Request.TestCases
{
	public class GetBoardByIdHostedService
	{

		public GetBoardByIdHostedService(IRequestBuilder<GetBoardByIdOptions> getByIdRequestBuilder,
			IRequestHandler<GetBoardByIdOptions> handler)
		{
			var options = new GetBoardByIdOptions(1);

			var request = getByIdRequestBuilder
				.PassOptions(options)
				.HandleWith(handler)
				.Build();

			request.ExecuteAsync();
		}

	}
}
