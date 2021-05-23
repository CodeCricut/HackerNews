using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Requests.GetEntityById;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.GetUserById
{
	public class GetUserByIdRequest : IRequest
	{
		public GetUserByIdRequest()
		{
		}

		public Task ExecuteAsync()
		{
			throw new NotImplementedException();
			GetEntityByIdRequestBuilder<GetBoardModel, BoardInclusionConfiguration> builder = new GetEntityByIdRequestBuilder();
			builder.OverrideInclusion
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
