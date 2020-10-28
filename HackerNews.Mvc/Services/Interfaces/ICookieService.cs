using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services.Interfaces
{
	/// <summary>
	/// Used to read and write any kind of cookie.
	/// </summary>
	public interface ICookieService
	{
		ICollection<string> Keys { get; }
		bool Contains(string key);
		string Get(string key);
		void Set(string key, string value, int? expireMinutes);
		void Remove(string key);
	}
}
