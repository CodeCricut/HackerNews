using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Other
{
	public class UserCardViewModel
	{
		public GetPublicUserModel User { get; set; }
	}
}
