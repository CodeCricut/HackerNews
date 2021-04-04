//using HackerNews.Domain.Common.Models.Users;
//using HackerNews.Domain.Entities;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hackernews.WPF.Services
//{
//	public interface ISignInManager
//	{
//		public Task SignInAsync(LoginModel loginModel);
//	}

//	public class WpfSignInManager : ISignInManager
//	{
//		private readonly SignInManager<User> _signInManager;
//		private readonly UserManager<User> _userManager;
//		private readonly IUserPrincipal _userPrincipal;

//		public WpfSignInManager(SignInManager<User> signInManager, UserManager<User> userManager, IUserPrincipal userPrincipal)
//		{
//			_signInManager = signInManager;
//			_userManager = userManager;
//			_userPrincipal = userPrincipal;
//		}

//		public async Task SignInAsync(LoginModel loginModel)
//		{
//			var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: false, false);

//			if (result.Succeeded)
//			{
//				var user = _userManager.Users.SingleOrDefault(u => u.UserName == loginModel.UserName);
//				_userPrincipal.SetUser(user);
//			}
//		}
//	}
//}
