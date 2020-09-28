using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public interface IAuthenticatableEntityHelper<TAuthenticateRequest, TAuthenticateResponse, TEntity, TPrivateReturnModel>
	{
		/// <summary>
		/// Attempt to retrieve a user from the database based on the credentials, then return null if not found or
		/// a new <typeparamref name="TAuthenticateResponse"/> with a valid JWT if valid.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public Task<TAuthenticateResponse> AuthenticateAsync(TAuthenticateRequest model);

		public Task<TPrivateReturnModel> GetAuthenticatedReturnModelAsync(HttpContext httpContext);
		
	}
}
