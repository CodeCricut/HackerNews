using AutoMapper;
using CleanEntityArchitecture.Authorization;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	public class AuthenticateUserService : IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel>
	{
		private readonly IMapper _mapper;
		private readonly IJwtHelper _jwtHelper;
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly IUserAuth<User> _userAuth;
		private readonly HttpContext _httpContext;

		public AuthenticateUserService(IMapper mapper, IJwtHelper jwtHelper, IUserLoginRepository userLoginRepository, IHttpContextAccessor contextAccessor, IUserAuth<User> userAuth)
		{
			_mapper = mapper;
			_jwtHelper = jwtHelper;
			_userLoginRepository = userLoginRepository;
			_userAuth = userAuth;
			_httpContext = contextAccessor.HttpContext;
		}

		/// <summary>
		/// Attempt to retrieve a user from the database based on the credentials, then return null if not found or
		/// a new <see cref="AuthenticateUserResponse"/> with a valid JWT if valid.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public virtual async Task<GetPrivateUserModel> AuthenticateAsync(LoginModel model)
		{
			var user = await _userLoginRepository.GetUserByCredentialsAsync(model.Username, model.Password);

			// return null if not found
			if (user == null) return null;

			// generate token if user found
			var token = _jwtHelper.GenerateJwtToken(user);

			var privateReturnModel = _mapper.Map<GetPrivateUserModel>(user);
			privateReturnModel.JwtToken = token;

			return privateReturnModel;
		}

		public virtual async Task<GetPrivateUserModel> GetAuthenticatedReturnModelAsync()
		{
			var user = await _userAuth.GetAuthenticatedUserAsync();

			var token = _jwtHelper.GenerateJwtToken(user);

			var privateReturnModel = _mapper.Map<GetPrivateUserModel>(user);
			privateReturnModel.JwtToken = token;

			return privateReturnModel;
		}
	}
}
