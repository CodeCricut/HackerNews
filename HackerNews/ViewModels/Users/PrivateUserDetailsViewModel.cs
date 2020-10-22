using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.ViewModels.Base;
using System.Collections;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Users
{
	public class PrivateUserDetailsViewModel : DetailsViewModel<GetPrivateUserModel>
	{
		public IEnumerable<GetArticleModel> Articles { get; set; }
		public IEnumerable<GetCommentModel> Comments { get; set; }
	}
}
