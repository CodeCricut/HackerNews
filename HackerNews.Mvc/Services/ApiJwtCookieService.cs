using HackerNews.Domain.Common.Models;
using HackerNews.Mvc.Services.Interfaces;

namespace HackerNews.Mvc.Services
{
	/// <summary>
	/// Interact with a JWT cookie with the "JWT" key.
	/// </summary>
	public class ApiJwtCookieService : IApiJwtCookieService
	{
		private static readonly string KEY = ".Api.Token.";

		private readonly ICookieService _cookieService;

		public ApiJwtCookieService(ICookieService cookieService)
		{
			_cookieService = cookieService;
		}

		public void SetToken(Jwt token, int expiresMinutes)
		{
			_cookieService.Set(KEY, token.Token, expiresMinutes);
		}

		public string GetToken()
		{
			return _cookieService.Get(KEY);
		}

		public bool ContainsToken()
		{
			return _cookieService.Contains(KEY);
		}

		public void RemoveToken()
		{
			_cookieService.Remove(KEY);
		}
	}
}
