using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.GetEntityById;
using HackerNews.CLI.Verbs.GetBoardById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Requests.EntityRequest
{
	public class GetBoardByIdRequestBuilder :
		RequestBuilder<GetBoardByIdRequest, GetBoardByIdOptions>
	{
		public GetBoardByIdRequestBuilder()
		{

		}

		protected override GetBoardByIdRequest BuildRequest()
		{
			return new GetBoardByIdRequest();
		}
	}
}
