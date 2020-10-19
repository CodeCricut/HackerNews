using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.ViewModels.Base;
using System.Collections.Generic;

namespace HackerNews.ViewModels
{
	public class ArticleDetailsViewModel : DetailsViewModel<GetArticleModel>
	{
		public GetPublicUserModel User { get; set; }
		public IEnumerable<GetCommentModel> Comments { get; set; }
		public GetBoardModel Board { get; set; }
		public PostCommentModel PostCommentModel { get; set; }
	}
}
