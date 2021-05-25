using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Loggers
{
	public interface IJwtLogger
	{
		void LogJwt(Jwt jwt);
	}

	public class JwtLogger : IJwtLogger
	{
		private readonly ILogger<JwtLogger> _logger;

		public JwtLogger(ILogger<JwtLogger> logger)
		{
			_logger = logger;
		}

		public void LogJwt(Jwt jwt)
		{
			_logger.LogInformation($"JWT: {jwt.Token} (expires at {jwt.Expires})");
		}
	}
}
