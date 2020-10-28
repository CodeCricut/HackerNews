using HackerNews.Application.Common.Models;
using HackerNews.Mvc.Services.Interfaces;

namespace HackerNews.Mvc.Services
{
	/// <summary>
	/// Interact with a JWT cookie with the "JWT" key.
	/// </summary>
	public class JwtCookieSetterService : IJwtSetterService
	{
		private readonly ICookieService _cookieService;

		public JwtCookieSetterService(ICookieService cookieService)
		{
			_cookieService = cookieService;
		}

		public void SetToken(Jwt token, int expiresMinutes)
		{
			_cookieService.Set("JWT", token.Token, expiresMinutes);
		}

		public string GetToken()
		{
			return _cookieService.Get("JWT");
		}

		public bool ContainsToken()
		{
			return _cookieService.Contains("JWT");
		}

		public void RemoveToken()
		{
			_cookieService.Remove("JWT");
		}
	}
}
