using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Api.Helpers.JWT;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class ModifyPrivateUserController : ModifyEntityController<User, RegisterUserModel, GetPrivateUserModel>
	{
		private readonly IJwtHelper _jwtHelper;

		public ModifyPrivateUserController(
			IModifyEntityService<User, RegisterUserModel, GetPrivateUserModel> modifyService, 
			IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService,
			IJwtHelper jwtHelper) : base(modifyService, userAuthService)
		{
			_jwtHelper = jwtHelper;
		}

		[HttpPost("register")]
		[Authorize(false)]
		public override async Task<ActionResult<GetPrivateUserModel>> PostAsync([FromBody] RegisterUserModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var response = await _modifyService.PostEntityModelAsync(postModel, null);

			// TODO: throw some unique exception
			if (response == null) throw new Exception();

			return Ok(response);
		}

		public override Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<RegisterUserModel> postModels)
		{
			throw new UnauthorizedException("Unauthorized to register mutliple users at once.");
		}
	}
}
