using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public interface IGetPublicUserProcessor : IGetVerbProcessor<RegisterUserModel, GetPublicUserModel>
	{

	}

	public class GetPublicUserProcessor : GetVerbProcessor<RegisterUserModel, GetPublicUserModel>,
		IGetPublicUserProcessor
	{
		public GetPublicUserProcessor(IEntityApiClient<RegisterUserModel, GetPublicUserModel> entityApiClient, IEntityLogger<GetPublicUserModel> entityLogger) 
			: base(entityApiClient, entityLogger)
		{
		}
	}
}
