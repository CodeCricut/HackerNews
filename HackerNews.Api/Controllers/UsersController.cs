using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Commands.DeleteUser;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Application.Users.Commands.SaveArticleToUser;
using HackerNews.Application.Users.Commands.SaveCommentToUser;
using HackerNews.Application.Users.Commands.UpdateUser;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Application.Users.Queries.GetPublicUsersByIds;
using HackerNews.Application.Users.Queries.GetPublicUsersWithPagination;
using HackerNews.Application.Users.Queries.GetUserByUsername;
using HackerNews.Application.Users.Queries.GetUsersBySearch;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Web.Pipeline.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : ApiController
	{
		private readonly IJwtGeneratorService _jwtGeneratorService;
		private readonly SignInManager<User> _signInManager;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public UsersController(IJwtGeneratorService jwtGeneratorService, SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager)
		{
			_jwtGeneratorService = jwtGeneratorService;
			_signInManager = signInManager;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpGet("me")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> GetAuthenticatedUser()
		{
			return Ok(await Mediator.Send(new GetAuthenticatedUserQuery()));
		}

		[HttpGet("jwt")]
		public async Task<ActionResult<Jwt>> GetJwt(LoginModel loginModel)
		{
			// Log in
			var userModel = await Mediator.Send(new GetUserByUsernameQuery(loginModel.Username));

			var publicUser = new User
			{
				UserName = userModel.Username,
				Id = userModel.Id
			};

			await _signInManager.PasswordSignInAsync(publicUser, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

			// Get auth user
			var loggedInUserModel = await Mediator.Send(new GetAuthenticatedUserQuery());
			var loggedInUser = _mapper.Map<User>(loggedInUserModel);

			// Return JWT
			return await _jwtGeneratorService.GenererateJwtFromUser(loggedInUser);
		}

		[HttpGet]
		public async Task<ActionResult<PaginatedList<GetPublicUserModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			return Ok(await Mediator.Send(new GetPublicUsersWithPaginationQuery(pagingParams)));
		}

		[HttpGet("{key:int}")]
		public async Task<ActionResult<GetPublicUserModel>> GetByIdAsync(int key)
		{
			return Ok(await Mediator.Send(new GetPublicUserQuery(key)));
		}

		[HttpGet("range")]
		public async Task<ActionResult<PaginatedList<GetPublicUserModel>>> GetByIdAsync([FromQuery] IEnumerable<int> id, [FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetPublicUsersByIdsQuery(id, pagingParams));
		}

		[HttpGet("search")]
		public async Task<ActionResult<PaginatedList<GetPublicUserModel>>> Search(string searchTerm, PagingParams pagingParams)
		{
			return await Mediator.Send(new GetUsersBySearchQuery(searchTerm, pagingParams));
		}

		[HttpPost("save-article")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveArticleAsync([FromQuery] int articleId)
		{
			return Ok(await Mediator.Send(new SaveArticleToUserCommand(articleId)));
		}

		[HttpPost("save-comment")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveCommentAsync([FromQuery] int commentId)
		{
			return Ok(await Mediator.Send(new SaveCommentToUserCommand(commentId)));

		}

		[HttpDelete]
		[JwtAuthorize]
		public async Task<ActionResult> Delete()
		{
			return Ok(await Mediator.Send(new DeleteCurrentUserCommand()));
		}

		[HttpPost]
		public async Task<ActionResult<GetPrivateUserModel>> PostAsync([FromBody] RegisterUserModel postModel)
		{
			return Ok(await Mediator.Send(new RegisterUserCommand(postModel)));
		}

		[HttpPut]
		public async Task<ActionResult<GetPrivateUserModel>> Put([FromBody] UpdateUserModel updateModel)
		{
			return Ok(await Mediator.Send(new UpdateUserCommand(updateModel)));
		}
	}
}
