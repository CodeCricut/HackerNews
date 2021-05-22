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
