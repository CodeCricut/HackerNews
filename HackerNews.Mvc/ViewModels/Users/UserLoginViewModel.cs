using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserLoginViewModel
	{
		public LoginModel LoginModel { get; set; }
		public string ReturnUrl { get; set; }
	}
}
