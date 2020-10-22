using AutoMapper;
using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Board;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.BoardServices
{
	public class WriteBoardService : WriteEntityService<Board, PostBoardModel>
	{
		private readonly IUserAuth<User> _cleanUserAuth;
		private readonly IBoardUserManagementService _boardUserManagementService;

		public WriteBoardService(IMapper mapper,
			IWriteEntityRepository<Board> writeRepo,
			IReadEntityRepository<Board> readRepo,
			IUserAuth<User> cleanUserAuth,
			IBoardUserManagementService boardUserManagementService) : base(mapper, writeRepo, readRepo)
		{
			_cleanUserAuth = cleanUserAuth;
			_boardUserManagementService = boardUserManagementService;
		}

		public override async Task<TGetModel> PostEntityModelAsync<TGetModel>(PostBoardModel entityModel)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();
			var entity = _mapper.Map<Board>(entityModel);

			entity.Creator = currentUser;
			entity.CreateDate = DateTime.UtcNow;


			var addedEntity = await _writeRepo.AddEntityAsync(entity);

			await _writeRepo.SaveChangesAsync();

			await _boardUserManagementService.AddBoardModeratorAsync(addedEntity.Id, currentUser.Id);

			return _mapper.Map<TGetModel>(addedEntity);
		}

		public override async Task<TGetModel> PutEntityModelAsync<TGetModel>(int id, PostBoardModel entityModel)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();
			// verify entity trying to update exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user has moderation privileges
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.Creator.Id != currentUser.Id ||
				entity.Moderators.FirstOrDefault(m => m.UserId == currentUser.Id) == null)
				throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Board>(entityModel);

			// update and save
			await _writeRepo.UpdateEntityAsync(id, updatedEntity);
			await _writeRepo.SaveChangesAsync();

			// return updated entity
			// TODO: see if ldkalkfj
			return _mapper.Map<TGetModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			// verify entity exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user created the board
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.Creator.Id != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _writeRepo.SoftDeleteEntityAsync(id);
			await _writeRepo.SaveChangesAsync();
		}
	}
}
