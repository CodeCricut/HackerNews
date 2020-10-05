using HackerNews.Domain.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Domain;
using AutoMapper;
using HackerNews.EF.Repositories;
using HackerNews.Domain.Errors;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class ModifyArticleService : ModifyEntityService<Domain.Article, PostArticleModel, GetArticleModel>
	{
		private readonly IMapper _mapper;
		private readonly IEntityRepository<Domain.Article> _entityRepository;

		public ModifyArticleService(IMapper mapper, IEntityRepository<Domain.Article> entityRepository)
		{
			_mapper = mapper;
			_entityRepository = entityRepository;
		}

		public override async Task<GetArticleModel> PostEntityModelAsync(PostArticleModel entityModel, User currentUser)
		{
			var entity = _mapper.Map<Domain.Article>(entityModel);

			entity.UserId = currentUser.Id;
			entity.PostDate = DateTime.UtcNow;

			var addedEntity = await _entityRepository.AddEntityAsync(entity);
			await _entityRepository.SaveChangesAsync();

			return _mapper.Map<GetArticleModel>(addedEntity);
		}

		public override async Task<GetArticleModel> PutEntityModelAsync(int id, PostArticleModel entityModel, User currentUser)
		{
			// verify entity trying to update exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();
			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			// verify new board belongs to prevoius board
			if (entityModel.BoardId != entity.BoardId) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Domain.Article>(entityModel);

			// update and save
			await _entityRepository.UpdateEntityAsync(id, updatedEntity);
			await _entityRepository.SaveChangesAsync();

			// return updated entity
			// TODO: make sure this actually updates the return
			return _mapper.Map<GetArticleModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id, User currentUser)
		{
			// verify entity exists
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _entityRepository.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
		}

	}
}
