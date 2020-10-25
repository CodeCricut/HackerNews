using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Users
{
	public class RegisterUserModel : PostModelDto, IMapFrom<User>
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

		public void Mapping(Profile profile)
		{
			profile.CreateMap<RegisterUserModel, User>();
		}
	}
}
