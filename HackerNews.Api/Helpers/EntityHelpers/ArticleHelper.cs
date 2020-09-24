using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF.Repositories;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class ArticleHelper : EntityHelper<Article, PostArticleModel, GetArticleModel>, IVoteableEntityHelper<Article>
	{
		public ArticleHelper(IEntityRepository<Article> entityRepository, IMapper mapper)
			: base(entityRepository, mapper)
		{
		}

		//public override void UpdateEntityProperties(Article article, Article newArticle)
		//{
		//	article = _mapper.Map<Article, Article>(newArticle);
		//	// this is messy, but a quick fix
		//	//article.Title = newArticle.Title;
		//	//article.Text = newArticle.Text;
		//	//article.Type = newArticle.Type;
		//	//article.AuthorName = newArticle.AuthorName;
		//}

		public async Task VoteEntityAsync(int id, bool upvote)
		{
			var article = await _entityRepository.GetEntityAsync(id);
			// await GetEntityAsync(id);
			article.Karma = upvote
				? article.Karma + 1
				: article.Karma - 1;

			await _entityRepository.UpdateEntityAsync(id, article);
			await _entityRepository.SaveChangesAsync();
		}

		public override async Task<GetArticleModel> GetEntityModelAsync(int id)
		{
			Article article = await _entityRepository.GetEntityAsync(id);
			// GetEntityAsync(id);

			//article = Trimmer.GetNewTrimmedArticle(article, false);

			return _mapper.Map<GetArticleModel>(article);
			//await _entityConverter.ConvertEntityAsync<GetArticleModel>(article);
		}
	}
}
