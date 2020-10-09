using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using HackerNews.ViewModels.Base;
using System.Collections.Generic;

namespace HackerNews.ViewModels
{
	public class ArticleDetailsViewModel : DetailsViewModel<GetArticleModel>
	{
		public IEnumerable<GetCommentModel> Comments { get; set; }
	}
}
