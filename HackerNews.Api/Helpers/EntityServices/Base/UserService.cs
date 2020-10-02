using AutoMapper;
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
	public abstract class UserService : EntityService<User, RegisterUserModel, GetPublicUserModel>
	{

		public UserService(IEntityRepository<User> userRepository, IMapper mapper) : base(userRepository, mapper)
		{
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

		public override async Task<GetPublicUserModel> PostEntityModelAsync(RegisterUserModel entityModel, User currentUser)
		{
			var entity = _mapper.Map<User>(entityModel);

			var currentDate = DateTime.UtcNow;
			entity.JoinDate = currentDate;

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetPublicUserModel>(addedEntity);
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
	}
}
