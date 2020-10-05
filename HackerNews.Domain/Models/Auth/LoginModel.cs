using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Auth
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
