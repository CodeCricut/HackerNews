using HackerNews.CLI.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.GetEntityRequest
{
	public interface IGetEntityRequest<TEntity>
	{
		Task<TEntity> GetEntityAsync();
	}

	public class GetEntityByIdRequest<TEntity> : IGetEntityRequest<TEntity>
	{
		private readonly int _id;
		private readonly IGetEntityRepository<TEntity> _getRepo;

		public GetEntityByIdRequest(int id, IGetEntityRepository<TEntity> getRepo)
		{
			_id = id;
			_getRepo = getRepo;
		}

		public Task<TEntity> GetEntityAsync()
		{
			return _getRepo.GetByIdAsync(_id);
		}
	}
}
