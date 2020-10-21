﻿using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Other
{
	public class NavbarViewModel
	{
		public bool LoggedIn { 
			get
			{
				return User != null && User.Id != 0;
			}
		}
		public GetPrivateUserModel User { get; set; }
		public string SearchTerm { get; set; }
	}
}