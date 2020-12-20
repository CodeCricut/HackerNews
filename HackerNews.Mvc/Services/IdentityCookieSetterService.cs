using HackerNews.Mvc.Services.Interfaces;

namespace HackerNews.Mvc.Services
{
	public interface IIdentityCookieSetterService
	{
		string GetIdentityCookie();
		void SetIdentityCookie(string identityCookie, int expiresMinutes);
		bool ContainsIdentityCookie();
		void RemoveIdentityCookie();
	}
	public class IdentityCookieSetterService : IIdentityCookieSetterService
	{
		private static readonly string IDENTITY_COOKIE_KEY = ".AspNetCore.Identity.Application";
		private readonly ICookieService _cookieService;

		public IdentityCookieSetterService(ICookieService cookieService)
		{
			_cookieService = cookieService;
		}

		public bool ContainsIdentityCookie()
		{
			return _cookieService.Contains(IDENTITY_COOKIE_KEY);
		}

		public string GetIdentityCookie()
		{
			return _cookieService.Get(IDENTITY_COOKIE_KEY);
		}

		public void RemoveIdentityCookie()
		{
			_cookieService.Remove(IDENTITY_COOKIE_KEY);
		}

		public void SetIdentityCookie(string identityCookie, int expiresMinutes)
		{
			_cookieService.Set(IDENTITY_COOKIE_KEY, identityCookie, expiresMinutes);
		}
	}
}
