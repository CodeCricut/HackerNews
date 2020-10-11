using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Base
{
	public abstract class ReadEntityController<TEntity, TGetEntityModel> : ControllerBase, IReadEntityController<TEntity, TGetEntityModel>
		where TEntity : DomainEntity
		where TGetEntityModel : GetModelDto
	{
		protected readonly IReadEntityService<TEntity, TGetEntityModel> _readService;

		public ReadEntityController(IReadEntityService<TEntity, TGetEntityModel> readService)
		{
			_readService = readService;
		}

		[HttpGet]
		public async Task<ActionResult<PagedList<TGetEntityModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			PagedList<TGetEntityModel> models = await _readService.GetAllEntityModelsAsync(pagingParams);
			return Ok(models);
		}

		[HttpGet("{key:int}")]
		public async Task<ActionResult<TGetEntityModel>> GetByIdAsync(int key)
		{
			var model = await _readService.GetEntityModelAsync(key);

			return Ok(model);
		}
	}
}
