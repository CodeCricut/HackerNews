//using HackerNews.Domain.Models.Users;
//using HackerNews.Helpers.ApiServices.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;

//namespace HackerNews.Helpers
//{
//	public class AuthorizeUserAttribute : AuthorizeAttribute, IAuthorizationFilter
//	{
//		public void OnAuthorization(AuthorizationFilterContext context)
//		{
//			var apiReader = context.HttpContext.RequestServices.GetService<IApiReader>();

//			var privateUser = apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me").Result;
//			var loggedIn = privateUser != null && privateUser.Id > 0;

//			if (loggedIn) return;
//			context.Result = new RedirectToActionResult("Login", "Users", null);
//		}
//	}
//}
