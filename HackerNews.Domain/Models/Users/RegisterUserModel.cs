using CleanEntityArchitecture.Domain;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Users
{
	public class RegisterUserModel : PostModelDto
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }

		public RegisterUserModel()
		{

		}
	}
}
