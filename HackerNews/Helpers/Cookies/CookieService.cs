using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace HackerNews.Helpers.Cookies
{
	public class CookieService : ICookieService
	{
		private readonly HttpContext _httpContext;

		public CookieService(IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext.HttpContext;

			if (_httpContext == null) throw new ArgumentNullException(nameof(_httpContext));
		}

		public ICollection<string> Keys => _httpContext.Request.Cookies.Keys;

		public bool Contains(string key)
		{
			return _httpContext.Request.Cookies.ContainsKey(key);
		}

		public string Get(string key)
		{
			var cookieValue = "";
			if (Contains(key))
				cookieValue = _httpContext.Request.Cookies[key];

			return cookieValue;
		}

		public void Remove(string key)
		{
			_httpContext.Response.Cookies.Delete(key);
		}

		public void Set(string key, string value, int? expireMinutes)
		{
			CookieOptions options = new CookieOptions();
			if (expireMinutes.HasValue)
				options.Expires = DateTime.Now.AddMinutes(expireMinutes.Value);
			else
				options.Expires = DateTime.Now.AddDays(7);

			options.IsEssential = true;

			_httpContext.Response.Cookies.Append(key, value, options);
		}
	}
}
