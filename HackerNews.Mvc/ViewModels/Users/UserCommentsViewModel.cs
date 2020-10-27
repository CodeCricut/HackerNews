using HackerNews.Application.Common.Models.Comments;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserCommentsViewModel
	{
		public FrontendPage<GetCommentModel> CommentPage { get; set; }
	}
}
