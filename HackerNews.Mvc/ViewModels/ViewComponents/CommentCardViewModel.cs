using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.ViewComponents
{
	public class CommentCardViewModel
	{
		public GetCommentModel Comment { get; set; }
		public GetArticleModel ParentArticle { get; set; }
		public GetCommentModel ParentComment { get; set; }
		public GetBoardModel Board { get; set; }
		public GetPublicUserModel User { get; set; }
	}
}
