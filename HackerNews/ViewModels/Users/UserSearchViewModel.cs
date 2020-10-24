using HackerNews.Domain;
using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Users
{
	public class UserSearchViewModel
	{
		public string SearchTerm { get; set; }
		public Page<GetPublicUserModel> UserPage { get; set; }
	}
}
