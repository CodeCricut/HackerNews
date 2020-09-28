﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNews.Domain.Models.Users
{
	public class RegisterUserModel : PostEntityModel
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
