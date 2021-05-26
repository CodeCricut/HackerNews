using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.DataAccess.EntityFinders;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class PublicUserFinder : EntityFinder<GetPublicUserModel, RegisterUserModel>
	{
		public PublicUserFinder(IEntityApiClient<RegisterUserModel, GetPublicUserModel> entityApiClient, ILogger<EntityFinder<GetPublicUserModel, RegisterUserModel>> logger) : base(entityApiClient, logger)
		{
		}
	}
}
