using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF
{
	public class ArticleCommentRepository : IArticleCommentRepository
	{
		private readonly HackerNewsContext _context;
		private readonly IArticleRepository _articleRepository;
		private readonly ICommentRepository _commentRepository;

		public ArticleCommentRepository(HackerNewsContext context, IArticleRepository articleRepository, ICommentRepository commentRepository)
		{
			_context = context;
			_articleRepository = articleRepository;
			_commentRepository = commentRepository;
		}

		public async Task<IEnumerable<Article>> GetArticlesWithChildrenAsync()
		{
			var articles = _context.Articles
					.ToList();

			// for these types of loops, we should really only query the ids instead of the whole entity. very slow
			for (int i = 0; i < articles.Count; i++)
			{
				articles[i] = await GetArticleWithChildrenAsync(articles[i].Id);
			}

			return articles;
		}

		public async Task<Article> GetArticleWithChildrenAsync(int id)
		{
				var article = await _context.Articles
				.Include(a => a.Comments)
				.SingleOrDefaultAsync(a => a.Id == id);

				// modify the comments to not have any parents (this seems to be deleting the relationship, see comment below)
				// TODO: fix the asynchronous stuff
				for (int i = 0; i < article.Comments.Count; i++)
				{
					var comment = article.Comments[i];
					// if this is deleting the relationship, we should get it with the parent and handle downstream
					comment = await GetCommentWithParentAsync(comment.Id, includeChildren: true); 
					//_commentRepository.GetCommentWithoutParentAsync(comment.Id, includeChildren: true);
				}
				return article;
		}

		public async Task<IEnumerable<Comment>> GetCommentsWithParentsAsync( bool includeChildren)
		{
			var comments = _context.Comments
					.ToList();

			for (int i = 0; i < comments.Count; i++)
			{
				comments[i] = await GetCommentWithParentAsync(comments[i].Id, includeChildren);
			}

			return comments;

		}

		public async Task<Comment> GetCommentWithParentAsync(int id, bool includeChildren)
		{
			var comment = await _context.Comments
				.Include(c => c.ParentComment)
				.Include(c => c.ParentArticle)
				.Include(c => c.Comments)
				.SingleOrDefaultAsync(c => c.Id == id);
			if (includeChildren)
			{
				for (int i = 0; i < comment.Comments.Count; i++)
				{
					// if this is deleting the relationship, we should get it with the parent and handle downstream
					// this will remove parent reference
					// comment.Comments[i] =
					// await _commentRepository.GetCommentWithoutParentAsync(comment.Comments[i].Id, includeChildren: true);

					// will result in circular reference which has to be resolved downstream
					comment.Comments[i] =
						await GetCommentWithParentAsync(comment.Comments[i].Id, includeChildren: true);
				}
			}
			else
			{
				comment.Comments = null;
			}
			// comment.ParentArticle =
			//	await _articleRepository.GetArticleWithoutChildrenAsync(comment.ParentArticle.Id);

			// will result in circular reference which has to be resolved downstream
			comment.ParentArticle =
				await GetArticleWithChildrenAsync(comment.ParentArticle.Id);

			return comment;
		}

	}
}
