using HackerNews.Api.Helpers.Attributes;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	interface IReadEntityController<TEntity, TGetModel>
		where TEntity : DomainEntity
		where TGetModel : GetEntityModel
	{
		[HttpGet]
		Task<ActionResult<PagedList<TGetModel>>> GetAsync([FromQuery] PagingParams pagingParams);

		[HttpGet("{key:int}")]
		Task<ActionResult<TGetModel>> GetByIdAsync(int key);
	}
}
