using HackerNews.Api.Converters;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class ArticleHelper : EntityHelper<Article, PostArticleModel, GetArticleModel>, IVoteableEntityHelper<Article>
	{
		public ArticleHelper(IEntityRepository<Article> entityRepository, IEntityConverter<Article, PostArticleModel, GetArticleModel> articleConverter) 
			: base(entityRepository, articleConverter)
		{
		}

		public override void UpdateEntityProperties(Article article, Article newArticle)
		{
			// this is messy, but a quick fix
			article.Title = newArticle.Title;
			article.Text = newArticle.Text;
			article.Type = newArticle.Type;
			article.AuthorName = newArticle.AuthorName;
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

		public override async Task<List<Article>> GetAllEntitiesAsync()
		{
			List<Article> articles = (await _entityRepository.GetEntitiesAsync()).ToList();

			return await Trimmer.GetNewTrimmedArticlesAsync(articles, false);
		}
	}
}
