using HackerNews.Application.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.ViewComponents
{
	public class UserCardViewModel
	{
		public GetPublicUserModel User { get; set; }
	}
}
