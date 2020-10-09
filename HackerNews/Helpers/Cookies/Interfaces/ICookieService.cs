using System.Collections.Generic;

namespace HackerNews.Helpers.Cookies.Interfaces
{
	public interface ICookieService
	{
		ICollection<string> Keys { get; }
		bool Contains(string key);
		string Get(string key);
		void Set(string key, string value, int? expireMinutes);
		void Remove(string key);
	}
}
