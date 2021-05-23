using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Request.Core
{
	public interface IRequestResponse
	{
		//Type ResponseType { get; }
		T GetResponse<T>();
	}
}
