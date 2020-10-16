using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class WritePrivateUserController : WriteController<User, RegisterUserModel, GetPrivateUserModel>
	{

		public WritePrivateUserController(
			IWriteEntityService<User, RegisterUserModel> modifyService,
			IJwtHelper jwtHelper) : base(modifyService)
		{
		}

		[HttpPost("register")]
		[JwtAuthorize(false)]
		public override async Task<ActionResult<GetPrivateUserModel>> PostAsync([FromBody] RegisterUserModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var response = await _writeService.PostEntityModelAsync<GetPrivateUserModel>(postModel);

			// TODO: throw some unique exception
			if (response == null) throw new Exception();

			return Ok(response);
		}


		public override Task<ActionResult<IEnumerable<GetPrivateUserModel>>> PostRangeAsync([FromBody] IEnumerable<RegisterUserModel> postModels)
		{
			throw new UnauthorizedException("Unauthorized to register mutliple users at once.");
		}
	}
}
