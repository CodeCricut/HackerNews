using HackerNews.Domain.Models.Comments;
using HackerNews.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Users
{
	public class UserCommentsListView : ListViewModel<GetCommentModel>
	{
		public UserCommentsListView()
		{
		}
	}
}
