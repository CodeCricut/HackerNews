using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.Attributes
{

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		private readonly bool _authorize;

		public AuthorizeAttribute(bool authorize = true)
		{
			_authorize = authorize;
		}

		/// <summary>
		/// Authorization is performed by the OnAuthorization method which checks if there is an authenticated user 
		/// attached to the current request (context.HttpContext.Items["User"]). An authenticated user is attached by 
		/// the custom jwt middleware if the request contains a valid JWT access token.
		/// 
		/// On successful authorization no action is taken and the request is passed through to the controller action method,
		/// if authorization fails a 401 Unauthorized response is returned.
		/// </summary>
		/// <param name="context"></param>
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!_authorize)
			{
				var user = (User)context.HttpContext.Items["User"];
				if (user == null)
				{
					// the user is not yet logged in, return 401

					// this isn't ideal, but throwing using actual exceptions causes the exception middleware to run and return the wrong exception
					context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
					// new JsonResult(new UnauthorizedException());
				}
			}
		}
	}
}
