using HackerNews.Helpers.Cookies.Interfaces;

namespace HackerNews.Helpers.Cookies
{
	public class JwtCookieService : IJwtService
	{
		private readonly ICookieService _cookieService;

		public JwtCookieService(ICookieService cookieService)
		{
			_cookieService = cookieService;
		}

		public void SetToken(string token, int expiresMinutes)
		{
			_cookieService.Set("JWT", token, expiresMinutes);
		}

		public string GetToken()
		{
			return _cookieService.Get("JWT");
		}

		public bool ContainsToken()
		{
			return _cookieService.Contains("JWT");
		}
	}
}
