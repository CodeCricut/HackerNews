using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Request.Core
{
	public interface IRequestHandler<TRequestOptions>
		where TRequestOptions : IRequestOptions
	{
		Task HandleAsync(TRequestOptions options);
	}
}
