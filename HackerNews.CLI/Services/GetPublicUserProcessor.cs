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
	public class GetPublicUserProcessor : GetVerbProcessor<GetPublicUserModel>
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IEntityLogger<GetPublicUserModel> _userLogger;

		public GetPublicUserProcessor(IUserApiClient userApiClient,
			IEntityLogger<GetPublicUserModel> userLogger)
		{
			_userApiClient = userApiClient;
			_userLogger = userLogger;
		}

		protected override Task<PaginatedList<GetPublicUserModel>> GetEntitiesAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _userApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		protected override Task<PaginatedList<GetPublicUserModel>> GetEntitiesAsync(PagingParams pagingParams)
		{
			return _userApiClient.GetPageAsync(pagingParams);
		}

		protected override Task<GetPublicUserModel> GetEntityAsync(int id)
		{
			return _userApiClient.GetByIdAsync(id);
		}

		protected override void OutputEntityPage(PaginatedList<GetPublicUserModel> entityPage)
		{
			_userLogger.LogEntityPage(entityPage);
		}

		protected override void OutputEntity(GetPublicUserModel entity)
		{
			_userLogger.LogEntity(entity);
		}
	}
}
