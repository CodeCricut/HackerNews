using AutoMapper;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	// TODO: the register user model is fine for updating the user for now, but should be a dedicated UpdateUserModel later
	public class ModifyPrivateUserService : ModifyEntityService<Domain.User, RegisterUserModel, GetPrivateUserModel>
	{
		private readonly IEntityRepository<Domain.User> _entityRepository;
		private readonly IMapper _mapper;

		public ModifyPrivateUserService(IEntityRepository<Domain.User> entityRepository, IMapper mapper)
		{
			_entityRepository = entityRepository;
			_mapper = mapper;
		}


		public override async Task<GetPrivateUserModel> PostEntityModelAsync(RegisterUserModel entityModel, Domain.User currentUser)
		{
			var entity = _mapper.Map<Domain.User>(entityModel);

			var currentDate = DateTime.UtcNow;
			entity.JoinDate = currentDate;

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetPrivateUserModel>(addedEntity);
		}


		public override Task PostEntityModelsAsync(List<RegisterUserModel> entityModels, Domain.User currentUser)
		{
			throw new UnauthorizedException("Not permitted to register mutliple users at once.");
		}

		public override async Task<GetPrivateUserModel> PutEntityModelAsync(int id, RegisterUserModel entityModel, Domain.User currentUser)
		{

			// verify entity trying to update exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.Id != currentUser.Id) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Domain.User>(entityModel);

			// update and save
			await _entityRepository.UpdateEntityAsync(id, updatedEntity);
			await _entityRepository.SaveChangesAsync();

			// return updated entity
			// TODO: verify this is returning an updated version
			return _mapper.Map<GetPrivateUserModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id, Domain.User currentUser)
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
