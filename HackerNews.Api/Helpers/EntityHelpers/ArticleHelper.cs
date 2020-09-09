using HackerNews.Api.Converters;
using HackerNews.Api.Converters.Trimmers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class ArticleHelper : EntityHelper<Article, PostArticleModel, GetArticleModel>, IVoteableEntityHelper<Article>
	{
		public ArticleHelper(EntityRepository<Article> entityRepository, ArticleConverter articleConverter) 
			: base(entityRepository, articleConverter)
		{
		}

		public override void UpdateEntityProperties(Article article, PostArticleModel articleModel)
		{
			// this is messy, but a quick fix
			article.Title = articleModel.Title;
			article.Text = articleModel.Text;
			article.Type = (ArticleType)Enum.Parse(typeof(ArticleType), articleModel.Type);
			article.AuthorName = articleModel.AuthorName;
		}

		public async Task VoteEntityAsync(int id, bool upvote)
		{
			var article = await GetEntityAsync(id);
			article.Karma = upvote
				? article.Karma + 1
				: article.Karma - 1;

			await _entityRepository.UpdateEntityAsync(id, article);
			await _entityRepository.SaveChangesAsync();
		}

		public override async Task<GetArticleModel> GetEntityModelAsync(int id)
		{
			Article article = await GetEntityAsync(id);

			article = Trimmer.GetNewTrimmedArticle(article, false);

			return await _entityConverter.ConvertEntityAsync<GetArticleModel>(article);
		}

		public override async Task<List<GetArticleModel>> GetAllEntityModelsAsync()
		{
			var articles = await GetAllEntitiesAsync();

			return await _entityConverter.ConvertEntitiesAsync<GetArticleModel>(articles);
		}

		public override async Task<List<Article>> GetAllEntitiesAsync()
		{
			List<Article> articles = (await _entityRepository.GetEntitiesAsync()).ToList();

			return await Trimmer.GetNewTrimmedArticlesAsync(articles, false);
		}
	}
}
