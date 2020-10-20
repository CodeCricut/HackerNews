using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Other
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
