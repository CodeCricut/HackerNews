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
	public class GetCommentProcessor : GetVerbProcessor<GetCommentModel>
	{
		private readonly ICommentApiClient _commentApiClient;
		private readonly IEntityLogger<GetCommentModel> _commentLogger;

		public GetCommentProcessor(ICommentApiClient commentApiClient,
			IEntityLogger<GetCommentModel> commentLogger)
		{
			_commentApiClient = commentApiClient;
			_commentLogger = commentLogger;
		}

		protected override Task<PaginatedList<GetCommentModel>> GetEntitiesAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _commentApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		protected override Task<PaginatedList<GetCommentModel>> GetEntitiesAsync(PagingParams pagingParams)
		{
			return _commentApiClient.GetPageAsync(pagingParams);
		}

		protected override Task<GetCommentModel> GetEntityAsync(int id)
		{
			return _commentApiClient.GetByIdAsync(id);
		}

		protected override void OutputEntityPage(PaginatedList<GetCommentModel> entityPage)
		{
			_commentLogger.LogEntityPage(entityPage);
		}

		protected override void OutputEntity(GetCommentModel entity)
		{
			_commentLogger.LogEntity(entity);
		}
	}
}
