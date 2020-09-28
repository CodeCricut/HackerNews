using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using HackerNews.EF.Repositories;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class ArticleHelper : EntityHelper<Article, PostArticleModel, GetArticleModel>, IVoteableEntityHelper<Article>
	{
		private readonly IEntityRepository<User> _userRepository;

		public ArticleHelper(IEntityRepository<Article> entityRepository, IMapper mapper, IEntityRepository<User> userRepository)
			: base(entityRepository, mapper)
		{
			_userRepository = userRepository;
		}
		

		public override async Task<GetArticleModel> PostEntityModelAsync(PostArticleModel entityModel, User currentUser)
		{
			var entity = _mapper.Map<Article>(entityModel);

			entity.UserId = currentUser.Id;

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

			var updatedEntity = _mapper.Map<Article>(entityModel);

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
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _entityRepository.SoftDeleteEntityAsync(id);
			await _entityRepository.SaveChangesAsync();
		}

		public async Task VoteEntityAsync(int id, bool upvote, User currentUser)
		{
			var article = await _entityRepository.GetEntityAsync(id);
			// await GetEntityAsync(id);
			article.Karma = upvote
				? article.Karma + 1
				: article.Karma - 1;

			await _entityRepository.UpdateEntityAsync(id, article);
			await _entityRepository.SaveChangesAsync();
		}
	}
}
