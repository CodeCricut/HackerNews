using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public class UserAuthService : IAuthenticatableEntityService<AuthenticateUserRequest, AuthenticateUserResponse, User, GetPrivateUserModel>
	{
		private readonly IMapper _mapper;
		private readonly IEntityRepository<User> _userRepository;
		private readonly IUserRepository _userLoginRepository;
		private readonly AppSettings _appSettings;

		public UserAuthService(IMapper mapper, IOptions<AppSettings> appSettings, IEntityRepository<User> userRepository, IUserRepository userLoginRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_userLoginRepository = userLoginRepository;
			_appSettings = appSettings.Value;
		}

		/// <summary>
		/// Attempt to retrieve a user from the database based on the credentials, then return null if not found or
		/// a new <see cref="AuthenticateUserResponse"/> with a valid JWT if valid.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public virtual async Task<AuthenticateUserResponse> AuthenticateAsync(AuthenticateUserRequest model)
		{
			// Todo: it is quite inefficient to request all entities and then sort. 
			var user = (await _userLoginRepository.GetUserByCredentialsAsync(model.Username, model.Password));

			// return null if not found
			if (user == null) return null;

			// generate token if user found
			var token = GenerateJwtToken(user);

			return new AuthenticateUserResponse(user, token);
		}

		public virtual async Task<GetPrivateUserModel> GetAuthenticatedReturnModelAsync(HttpContext httpContext)
		{
			var user = await GetAuthenticatedUser(httpContext);
			return _mapper.Map<GetPrivateUserModel>(user);
		}

		public async Task<User> GetAuthenticatedUser(HttpContext httpContext)
		{
			return await Task.Factory.StartNew(() => (User)httpContext.Items["User"]);
		}


		private string GenerateJwtToken(User user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
