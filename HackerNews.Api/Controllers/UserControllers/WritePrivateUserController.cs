using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Pipeline.Filters;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class WritePrivateUserController : ApiController, IWriteEntityController<User, RegisterUserModel, GetPrivateUserModel>
	{
		public Task<ActionResult> Delete(int key)
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<GetPrivateUserModel>> PostAsync([FromBody] RegisterUserModel postModel)
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<RegisterUserModel> postModels)
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<GetPrivateUserModel>> Put(int key, [FromBody] RegisterUserModel updateModel)
		{
			throw new NotImplementedException();
		}
	}
}
