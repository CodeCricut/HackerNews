﻿using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class UserHelper : EntityHelper<User, RegisterUserModel, GetPublicUserModel>,
		IAuthenticatableEntityHelper<AuthenticateUserRequest, AuthenticateUserResponse, User, GetPrivateUserModel>
	{
		private readonly AppSettings _appSettings;

		public UserHelper(IEntityRepository<User> userRepository, IMapper mapper, IOptions<AppSettings> appSettings) : base(userRepository, mapper)
		{
			_appSettings = appSettings.Value;
		}

		/// <summary>
		/// Attempt to retrieve a user from the database based on the credentials, then return null if not found or
		/// a new <see cref="AuthenticateUserResponse"/> with a valid JWT if valid.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<AuthenticateUserResponse> AuthenticateAsync(AuthenticateUserRequest model)
		{
			// Todo: it is quite inefficient to request all entities and then sort. 
			var user = (await _entityRepository.GetEntitiesAsync()).SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);

			// return null if not found
			if (user == null) return null;

			// generate token if user found
			var token = GenerateJwtToken(user);

			return new AuthenticateUserResponse(user, token);
		}

		public async Task<GetPrivateUserModel> GetAuthenticatedReturnModelAsync(HttpContext httpContext)
		{
			var user = await GetAuthenticatedUser(httpContext);
			return _mapper.Map<GetPrivateUserModel>(user);
		}

		public async Task<User> GetAuthenticatedUser(HttpContext httpContext)
		{
			return await Task.Factory.StartNew(() => (User)httpContext.Items["User"]);
		}

		// this gets messy... userhelper shouldn't be forced to implement these
		public override async Task<GetPublicUserModel> PostEntityModelAsync(RegisterUserModel entityModel, User currentUser)
		{
			var entity = _mapper.Map<User>(entityModel);

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetPublicUserModel>(addedEntity);
		}

		public override async Task<GetPublicUserModel> PutEntityModelAsync(int id, RegisterUserModel entityModel, User currentUser)
		{
			// verify entity trying to update exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.Id != currentUser.Id) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<User>(entityModel);

			// update and save
			await _entityRepository.UpdateEntityAsync(id, updatedEntity);
			await _entityRepository.SaveChangesAsync();

			// return updated entity
			return await GetEntityModelAsync(id);
		}

		public override async Task SoftDeleteEntityAsync(int id, User currentUser)
		{
			// verify entity exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.Id != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
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
