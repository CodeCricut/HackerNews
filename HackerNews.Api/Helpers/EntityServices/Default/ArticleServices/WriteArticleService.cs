using AutoMapper;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class WriteArticleService : WriteEntityService<Article, PostArticleModel>
	{
		private readonly IMapper _mapper;
		private readonly IReadEntityRepository<Article> _readRepo;
		private readonly IWriteEntityRepository<Article> _writeRepo;
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuthService;
		private readonly HttpContext _httpContext;

		public WriteArticleService(IMapper mapper,
			IReadEntityRepository<Article> readRepo,
			IWriteEntityRepository<Article> writeRepo,
			IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService)
		{
			_mapper = mapper;
			_readRepo = readRepo;
			_writeRepo = writeRepo;
			_userAuthService = userAuthService;
		}


		public override async Task<TGetModel> PostEntityModelAsync<TGetModel>(PostArticleModel entityModel)
		{
			var currentUser = _userAuthService.GetAuthenticatedUser();

			var entity = _mapper.Map<Domain.Article>(entityModel);

			entity.UserId = currentUser.Id;
			entity.PostDate = DateTime.UtcNow;

			var addedEntity = await _writeRepo.AddEntityAsync(entity);
			await _writeRepo.SaveChangesAsync();

			return _mapper.Map<TGetModel>(addedEntity);
		}

		public override async Task<TGetModel> PutEntityModelAsync<TGetModel>(int id, PostArticleModel entityModel)
		{
			var currentUser = _userAuthService.GetAuthenticatedUser();

			// verify entity trying to update exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();
			// verify user owns the entity
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			// verify new board belongs to prevoius board
			if (entityModel.BoardId != entity.BoardId) throw new UnauthorizedException();

			var updatedEntity = _mapper.Map<Article>(entityModel);

			// update and save
			await _writeRepo.UpdateEntityAsync(id, updatedEntity);
			await _writeRepo.SaveChangesAsync();

			// return updated entity
			// TODO: make sure this actually updates the return
			return _mapper.Map<TGetModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id)
		{
			var currentUser = _userAuthService.GetAuthenticatedUser();

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
