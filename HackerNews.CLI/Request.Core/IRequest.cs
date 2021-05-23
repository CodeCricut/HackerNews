using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Request.Core
{
	public interface IRequest<TOptions>
		where TOptions : IRequestOptions
	{
		Task ExecuteAsync();
		Task CancelAsync(CancellationToken cancellationToken);
	}
}
