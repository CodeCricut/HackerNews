using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleDetailsViewModel
	{
		public GetArticleModel Article { get; set; }
		public FrontendPage<GetCommentModel> CommentPage { get; set; }

		public GetBoardModel Board { get; set; }
		public PostCommentModel PostCommentModel { get; set; }

		public GetPublicUserModel User { get; set; }
		public bool LoggedIn { get; set; }
		public bool UserSavedArticle { get; set; }

		public bool UserWroteArticle { get; set; }
	}
}
