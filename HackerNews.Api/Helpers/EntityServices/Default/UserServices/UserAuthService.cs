using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.JWT;
using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	public class UserAuthService : IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel>
	{
		private readonly IMapper _mapper;
		private readonly IJwtHelper _jwtHelper;
		private readonly IUserLoginRepository _userLoginRepository;
		private readonly HttpContext _httpContext;

		public UserAuthService(IMapper mapper, IJwtHelper jwtHelper, IUserLoginRepository userLoginRepository, IHttpContextAccessor contextAccessor)
		{
			_mapper = mapper;
			_jwtHelper = jwtHelper;
			_userLoginRepository = userLoginRepository;
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
			// Todo: it is quite inefficient to request all entities and then sort.
			var user = await _userLoginRepository.GetUserByCredentialsAsync(model.Username, model.Password);

			// return null if not found
			if (user == null) return null;

			// generate token if user found
			var token = _jwtHelper.GenerateJwtToken(user);

			var privateReturnModel = _mapper.Map<GetPrivateUserModel>(user);
			_jwtHelper.AttachJwtToken(ref privateReturnModel, token);

			return privateReturnModel;
		}

		public virtual async Task<GetPrivateUserModel> GetAuthenticatedReturnModelAsync()
		{
			var user = await GetAuthenticatedUser();

			var token = _jwtHelper.GenerateJwtToken(user);

			var privateReturnModel = _mapper.Map<GetPrivateUserModel>(user);
			_jwtHelper.AttachJwtToken(ref privateReturnModel, token);

			return privateReturnModel;
		}

		public async Task<User> GetAuthenticatedUser()
		{
			return await Task.Factory.StartNew(() => (User)_httpContext.Items["User"]);
		}

	}
}
