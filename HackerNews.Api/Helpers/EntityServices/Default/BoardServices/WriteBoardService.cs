using AutoMapper;
using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.BoardServices
{
	public class WriteBoardService : WriteEntityService<Board, PostBoardModel>
	{
		private readonly IMapper _mapper;
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuth;
		private readonly IWriteEntityRepository<Board> _writeBoardRepo;
		private readonly IReadEntityRepository<Board> _readBoardRepo;
		private readonly IUserAuth<User> _cleanUserAuth;

		public WriteBoardService(IMapper mapper,
			IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuth,
			IWriteEntityRepository<Board> writeBoardRepo,
			IReadEntityRepository<Board> readBoardRepo,
			IUserAuth<User> cleanUserAuth)
		{
			_mapper = mapper;
			_userAuth = userAuth;
			_writeBoardRepo = writeBoardRepo;
			_readBoardRepo = readBoardRepo;
			_cleanUserAuth = cleanUserAuth;
		}

		public override async Task<TGetModel> PostEntityModelAsync<TGetModel>(PostBoardModel entityModel)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();
			var entity = _mapper.Map<Board>(entityModel);

			entity.Creator = currentUser;
			entity.CreateDate = DateTime.UtcNow;

			var addedEntity = await _writeBoardRepo.AddEntityAsync(entity);

			await _writeBoardRepo.SaveChangesAsync();

			return _mapper.Map<TGetModel>(addedEntity);
		}

		public override async Task<TGetModel> PutEntityModelAsync<TGetModel>(int id, PostBoardModel entityModel)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();
			// verify entity trying to update exists
			if (!await _readBoardRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user has moderation privileges
			var entity = await _readBoardRepo.GetEntityAsync(id);
			if (entity.Creator.Id != currentUser.Id ||
				entity.Moderators.FirstOrDefault(m => m.UserId == currentUser.Id) == null)
				throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Board>(entityModel);

			// update and save
			await _writeBoardRepo.UpdateEntityAsync(id, updatedEntity);
			await _writeBoardRepo.SaveChangesAsync();

			// return updated entity
			// TODO: see if ldkalkfj
			return _mapper.Map<TGetModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			// verify entity exists
			if (!await _readBoardRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user created the board
			var entity = await _readBoardRepo.GetEntityAsync(id);
			if (entity.Creator.Id != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _writeBoardRepo.SoftDeleteEntityAsync(id);
			await _writeBoardRepo.SaveChangesAsync();
		}
	}
}
