﻿using AutoMapper;
using HackerNews.Api.Profiles;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.DB_Helpers
{
	class ArticleHelper : IArticleHelper
	{
		private readonly IArticleRepository _articleRepository;
		private readonly IMapper _mapper;

		public ArticleHelper(IArticleRepository articleRepository, IMapper mapper)
		{
			_articleRepository = articleRepository;
			_mapper = mapper;
		}

		public async Task<List<GetArticleModel>> GetAllArticleModels()
		{
			try
			{
				// actually gets with children
				var articles = (await _articleRepository.GetArticlesAsync()).ToList();
				TrimArticles(articles);

				return _mapper.Map<List<GetArticleModel>>(articles);
			}
			catch (Exception e)
			{
				// TODO: throw unique exceptions
				throw;
			}
		}

		public async Task<GetArticleModel> GetArticleModel(int id)
		{
			var article = (await _articleRepository.GetArticleAsync(id));
			TrimArticle(article);
			
			return _mapper.Map<GetArticleModel>(article);
		}

		public async void PostArticleModel(PostArticleModel articleModel)
		{
			Article article = _mapper.Map<Article>(articleModel);
			_articleRepository.AddArticle(article);
			await _articleRepository.SaveChangesAsync();
		}

		public async Task<GetArticleModel> PutArticleModel(int id, PostArticleModel articleModel)
		{
			var article = await _articleRepository.GetArticleAsync(id);
			UpdateArticleProperties(articleModel, article);

			_articleRepository.UpdateArticle(id, article);
			await _articleRepository.SaveChangesAsync();

			article = await _articleRepository.GetArticleAsync(id);
			TrimArticle(article);

			return _mapper.Map<GetArticleModel>(article);
		}

		public async void DeleteArticle(int id)
		{
			_articleRepository.DeleteArticle(id);
			await _articleRepository.SaveChangesAsync();
		}

		public async void VoteArticle(int id, bool upvote)
		{
			var article = await _articleRepository.GetArticleAsync(id);
			article.Karma = upvote
				? article.Karma + 1
				: article.Karma - 1;

			_articleRepository.UpdateArticle(id, article);
			await _articleRepository.SaveChangesAsync();
		}

		private static void TrimArticle(Article article)
		{
			article = EntityTrimmer.GetNewTrimmedArticle(article, false);
		}

		private static void TrimArticles(List<Article> articles)
		{
			for (int i = 0; i < articles.Count; i++)
			{
				var article = articles[i];
				TrimArticle(article);
			}
		}

		private static void UpdateArticleProperties(PostArticleModel articleModel, Article article)
		{
			// this is messy, but a quick fix
			article.Title = articleModel.Title;
			article.Text = articleModel.Text;
			article.Type = (ArticleType)Enum.Parse(typeof(ArticleType), articleModel.Type);
			article.AuthorName = articleModel.AuthorName;
		}
	}
}
