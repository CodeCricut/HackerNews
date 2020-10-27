using HackerNews.Application.Common.Models.Users;
using HackerNews.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetPublicUserModel> UserPage { get; set; }
	}
}
