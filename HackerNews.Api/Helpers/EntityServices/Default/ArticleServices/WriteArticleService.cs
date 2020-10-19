﻿using AutoMapper;
using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class WriteArticleService : WriteEntityService<Article, PostArticleModel>
	{
		private readonly IUserAuth<User> _userAuth;

		public WriteArticleService(IMapper mapper,
			IReadEntityRepository<Article> readRepo,
			IWriteEntityRepository<Article> writeRepo, IUserAuth<User> userAuth)
			:base(mapper, writeRepo, readRepo)
		{
			_userAuth = userAuth;
		}


		public override async Task<TGetModel> PostEntityModelAsync<TGetModel>(PostArticleModel entityModel)
		{
			var currentUser = await _userAuth.GetAuthenticatedUserAsync();

			var entity = _mapper.Map<Domain.Article>(entityModel);

			entity.UserId = currentUser.Id;
			entity.PostDate = DateTime.UtcNow;

			var addedEntity = await _writeRepo.AddEntityAsync(entity);
			await _writeRepo.SaveChangesAsync();

			return _mapper.Map<TGetModel>(addedEntity);
		}

		// TODO: refactor omg
		public override async Task<TGetModel> PutEntityModelAsync<TGetModel>(int id, PostArticleModel entityModel)
		{
			var currentUser = await _userAuth.GetAuthenticatedUserAsync();

			// verify entity trying to update exists
			if (!await _readRepo.VerifyExistsAsync(id)) throw new NotFoundException();
			// verify user owns the entity
			var entity = await _readRepo.GetEntityAsync(id);
			if (entity.UserId != currentUser.Id) throw new UnauthorizedException();

			var entityFromModel = _mapper.Map<Article>(entityModel);

			var updatedEntity = _mapper.Map<Article>(entity);

			// Only update the properties sent via the model
			updatedEntity.BoardId = entityModel.BoardId;
			updatedEntity.Text = entityModel.Text;
			updatedEntity.Title = entityModel.Title;
			updatedEntity.Type = entityFromModel.Type;


			// update and save
			await _writeRepo.UpdateEntityAsync(id, updatedEntity);
			await _writeRepo.SaveChangesAsync();

			// return updated entity
			// TODO: make sure this actually updates the return
			return _mapper.Map<TGetModel>(updatedEntity);
		}

		public override async Task SoftDeleteEntityAsync(int id)
		{
			var currentUser = await _userAuth.GetAuthenticatedUserAsync();

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