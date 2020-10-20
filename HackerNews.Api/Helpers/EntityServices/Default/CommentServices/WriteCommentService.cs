using AutoMapper;
using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using System;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class WriteCommentService : WriteEntityService<Comment, PostCommentModel>
	{
		private readonly IReadEntityRepository<Article> _readArticleRepo;
		private readonly IUserAuth<User> _cleanUserAuth;

		public WriteCommentService(IMapper mapper,
			IReadEntityRepository<Comment> readRepo,
			IWriteEntityRepository<Comment> writeRepo,
			IReadEntityRepository<Article> readArticleRepo,
			IUserAuth<User> cleanUserAuth) : base(mapper, writeRepo, readRepo)
		{
			_readArticleRepo = readArticleRepo;
			_cleanUserAuth = cleanUserAuth;
		}

		public override async Task<TGetModel> PostEntityModelAsync<TGetModel>(PostCommentModel entityModel)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			// TODO: add checks to the parents
			//		verify one and only one parent exists
			//		verify parent belongs to boardid/board exists
			var entity = _mapper.Map<Comment>(entityModel);


			var parentArticle = await _readArticleRepo.GetEntityAsync(entityModel.ParentArticleId);
			var parentComment = await _readRepo.GetEntityAsync(entityModel.ParentCommentId);

			if (parentArticle == null &&
				parentComment == null) throw new InvalidPostException("No parent given.");
			if (parentArticle != null &&
				parentComment != null) throw new InvalidPostException("Two parents given.");

			var boardId = 0;
			// God save us
			boardId = parentArticle != null
					? parentArticle.BoardId
					: (
					parentComment != null
					? parentComment.BoardId
					: 0
					);


			entity.UserId = currentUser.Id;
			entity.PostDate = DateTime.UtcNow;
			entity.BoardId = boardId;

			var addedEntity = await _writeRepo.AddEntityAsync(entity);
			await _writeRepo.SaveChangesAsync();

			return _mapper.Map<TGetModel>(addedEntity);
		}

		public override async Task<TGetModel> PutEntityModelAsync<TGetModel>(int id, PostCommentModel entityModel)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();
			// TODO: verify parents are the same as previously

			// verify entity trying to update exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();
			// verify user owns the entity
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Comment>(entityModel);

			// update and save
			await _writeRepo.UpdateEntityAsync(id, updatedEntity);
			await _writeRepo.SaveChangesAsync();

			// return updated entity
			// TODO: ddd
			return _mapper.Map<TGetModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			// verify entity exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();

			// verify user owns the entity
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			// soft delete and save
			await _writeRepo.SoftDeleteEntityAsync(id);
			await _writeRepo.SaveChangesAsync();
		}
	}
}
