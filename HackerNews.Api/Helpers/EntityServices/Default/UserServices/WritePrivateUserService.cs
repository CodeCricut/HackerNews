using AutoMapper;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	// TODO: the register user model is fine for updating the user for now, but should be a dedicated UpdateUserModel later
	public class WritePrivateUserService : WriteEntityService<User, RegisterUserModel>
	{
		private readonly IMapper _mapper;
		private readonly IReadEntityRepository<User> _readRepo;
		private readonly IWriteEntityRepository<User> _writeRepo;
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuth;

		public WritePrivateUserService(IMapper mapper, IReadEntityRepository<User> readRepo, IWriteEntityRepository<User> writeRepo,
			IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuth)
		{
			_mapper = mapper;
			_readRepo = readRepo;
			_writeRepo = writeRepo;
			_userAuth = userAuth;
		}

		public override async Task<TGetModel> PostEntityModelAsync<TGetModel>(RegisterUserModel entityModel)
		{
			var currentUser = await _userAuth.GetAuthenticatedUser();

			var entity = _mapper.Map<User>(entityModel);

			var currentDate = DateTime.UtcNow;
			entity.JoinDate = currentDate;

			var addedEntity = await _writeRepo.AddEntityAsync(entity);
			await _writeRepo.SaveChangesAsync();

			return _mapper.Map<TGetModel>(addedEntity);
		}

		public override Task PostEntityModelsAsync(List<RegisterUserModel> entityModels)
		{
			throw new UnauthorizedException("Not permitted to register mutliple users at once.");

		}

		public override async Task<TGetModel> PutEntityModelAsync<TGetModel>(int id, RegisterUserModel entityModel)
		{
			var currentUser = await _userAuth.GetAuthenticatedUser();

			// verify entity trying to update exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.Id != currentUser.Id) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Domain.User>(entityModel);

			// update and save
			await _writeRepo.UpdateEntityAsync(id, updatedEntity);
			await _writeRepo.SaveChangesAsync();

			// return updated entity
			// TODO: verify this is returning an updated version
			return _mapper.Map<TGetModel>(updatedEntity);
		}


		public override async Task SoftDeleteEntityAsync(int id)
		{
			var currentUser = await _userAuth.GetAuthenticatedUser();

			// verify entity exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.Id != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _writeRepo.SoftDeleteEntityAsync(id);
			await _writeRepo.SaveChangesAsync();
		}
	}
}
