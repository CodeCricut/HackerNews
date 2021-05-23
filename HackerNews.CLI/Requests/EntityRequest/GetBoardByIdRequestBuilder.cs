using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.GetEntityById;
using HackerNews.CLI.Verbs.GetBoardById;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Requests.EntityRequest
{
	public class GetBoardByIdRequestBuilder :
		RequestBuilder<GetEntityByIdRequest<GetBoardModel>, GetBoardByIdOptions>
	{
		public GetBoardByIdRequestBuilder()
		{

		}

		protected override GetBoardByIdRequest BuildRequest()
		{
			return new GetEntityByIdRequest<GetBoardModel>(
				_logger,
				_verbositySetter,
				_entityLogger,
				_entityWriter,
				_getEntityRepo,
				Options);
		}
	}
}
