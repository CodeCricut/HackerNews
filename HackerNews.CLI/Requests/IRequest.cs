using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests
{
	public interface IRequest
	{
		Task ExecuteAsync();
		Task CancelAsync(CancellationToken cancellationToken);
	}
}
