﻿using HackerNews.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace HackerNews.Web.Pipeline.Filters
{
	// *Filters* in ASP.NET Core allow code to be run before or after specific stages in the request processing pipeline.
	// Filters can only indirectly affect a controller when they are explicitly used by the controller.
	// They are run during the filter pipelien, after before and after (middleware and action selection)
	// They run in the following stages:
	//		Authorization, Resource, Action, Exception, Result
	/// <summary>
	/// Verify the the UserId is attatched to the context. Works in conjunction with <see cref="Middleware.JwtMiddleware"/>, which attatches
	/// the UserId when the request contains a valid JWT.
	/// </summary>
	[Obsolete("This attribute has been swapped out for the [Authorize] attribute as part of the Identity framework.")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		private readonly bool _authorize;

		public JwtAuthorizeAttribute(bool authorize = true)
		{
			_authorize = authorize;
		}

		/// <summary>
		/// Authorization is performed by the OnAuthorization method which checks if there is an authenticated user 
		/// attached to the current request (context.HttpContext.Items["UserId"]). An authenticated user is attached by 
		/// the custom jwt middleware if the request contains a valid JWT access token.
		/// 
		/// On successful authorization no action is taken and the request is passed through to the controller action method,
		/// if authorization fails a 401 Unauthorized response is returned.
		/// </summary>
		/// <param name="context"></param>
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (_authorize)
			{
				//var userId = context.HttpContext.Items["UserId"];
				ClaimsPrincipal user = context.HttpContext.User;
				string userId = user.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value;
				if (string.IsNullOrEmpty(userId))
					throw new UnauthorizedException();
			}
		}
	}
}
