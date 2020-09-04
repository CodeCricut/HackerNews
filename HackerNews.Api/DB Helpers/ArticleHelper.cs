using AutoMapper;
using HackerNews.Api.Profiles;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
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

		#region Create
		public async Task<GetArticleModel> PostArticleModelAsync(PostArticleModel articleModel)
		{
			Article article = await ConvertPostModelAsync(articleModel);

			var addedArticle = await _articleRepository.AddArticleAsync(article);
			await _articleRepository.SaveChangesAsync();

			return _mapper.Map<GetArticleModel>(addedArticle);
		}

		public async Task PostArticleModelsAsync(List<PostArticleModel> articleModels)
		{
			List<Article> articles = await ConvertPostModelsAsync(articleModels);
			await _articleRepository.AddArticlesAsync(articles);
			await _articleRepository.SaveChangesAsync();
		}
		#endregion

		#region Read
		public async Task<GetArticleModel> GetArticleModelAsync(int id)
		{
			Article article = await GetArticleAsync(id);

			TrimArticle(article);

			return _mapper.Map<GetArticleModel>(article);
		}

		public async Task<List<GetArticleModel>> GetAllArticleModelsAsync()
		{
			// actually gets with children
			var articles = (await _articleRepository.GetArticlesAsync()).ToList();
			TrimArticles(articles);

			return _mapper.Map<List<GetArticleModel>>(articles);
		}
		#endregion

		#region Update
		public async Task<GetArticleModel> PutArticleModelAsync(int id, PostArticleModel articleModel)
		{
			var article = await GetArticleAsync(id);

			UpdateArticleProperties(articleModel, article);

			await _articleRepository.UpdateArticleAsync(id, article);
			await _articleRepository.SaveChangesAsync();

			article = await GetArticleAsync(id);
			TrimArticle(article);

			return _mapper.Map<GetArticleModel>(article);
		}

		public async Task VoteArticleAsync(int id, bool upvote)
		{
			var article = await GetArticleAsync(id);
			article.Karma = upvote
				? article.Karma + 1
				: article.Karma - 1;

			await _articleRepository.UpdateArticleAsync(id, article);
			await _articleRepository.SaveChangesAsync();
		}
		#endregion

		#region Delete
		public async Task DeleteArticleAsync(int id)
		{
			// verify the article exists
			await GetArticleAsync(id);

			await _articleRepository.DeleteArticleAsync(id);
			await _articleRepository.SaveChangesAsync();
		}
		#endregion

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

		private async Task<Article> GetArticleAsync(int id)
		{
			var article = await _articleRepository.GetArticleAsync(id);
			if (article == null) throw new NotFoundException("The article with the given ID could not be found. Please ensure that the ID is valid.");

			return article;
		}

		private async Task<Article> ConvertPostModelAsync(PostArticleModel articleModel)
		{
			try
			{
				return await Task.Factory.StartNew(() =>  _mapper.Map<Article>(articleModel) );
			}
			catch (AutoMapperMappingException e)
			{
				throw new InvalidPostException(e.Message);
			}
		}

		private async Task<List<Article>> ConvertPostModelsAsync(List<PostArticleModel> articleModels)
		{
			var articleConversionTasks = new List<Task<Article>>();
			foreach (var articleModel in articleModels)
			{
				articleConversionTasks.Add(ConvertPostModelAsync(articleModel));
			}

			List<Article> articles = (await Task.WhenAll(articleConversionTasks)).ToList();
			return articles;
		}
	}
}
