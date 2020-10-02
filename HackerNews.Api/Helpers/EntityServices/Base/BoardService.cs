using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Board;
using HackerNews.EF.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public abstract class BoardService : EntityService<Board, PostBoardModel, GetBoardModel>
	{
		public BoardService(IEntityRepository<Board> entityRepository, IMapper mapper) : base(entityRepository, mapper)
		{
		}

		public override async Task<GetBoardModel> PostEntityModelAsync(PostBoardModel entityModel, User currentUser)
		{
			var entity = _mapper.Map<Board>(entityModel);

			entity.Creator = currentUser;
			entity.CreateDate = DateTime.UtcNow;

			var addedEntity = await _entityRepository.AddEntityAsync(entity);

			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetBoardModel>(addedEntity);
		}

		public override async Task<GetBoardModel> PutEntityModelAsync(int id, PostBoardModel entityModel, User currentUser)
		{
			// verify entity trying to update exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user has moderation privileges
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.Creator.Id != currentUser.Id ||
				entity.Moderators.FirstOrDefault(m => m.UserId == currentUser.Id) == null) 
					throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Board>(entityModel);

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

			// verify user created the board
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.Creator.Id != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
		}
	}
}
