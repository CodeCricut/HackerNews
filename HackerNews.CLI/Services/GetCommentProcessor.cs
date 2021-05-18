using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public interface IGetCommentProcessor : IGetVerbProcessor<PostCommentModel, GetCommentModel>
	{

	}

	public class GetCommentProcessor : GetVerbProcessor<PostCommentModel, GetCommentModel>, IGetCommentProcessor
	{
		public GetCommentProcessor(IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient, IEntityLogger<GetCommentModel> entityLogger) 
			: base(entityApiClient, entityLogger)
		{
		}
	}
}
