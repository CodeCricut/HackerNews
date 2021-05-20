using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Configuration
{
	public interface IVerbositySetter
	{
		void SetVerbository(bool verboseOutput);
		//void Configure(Action<AppConfigurationOptions> configureOpts);
	}

	public class VerbositySetter : IVerbositySetter
	{
		private readonly ILogger<VerbositySetter> _logger;
		private readonly LoggingLevelSwitch _loggingLevelSwitch;

		public VerbositySetter(ILogger<VerbositySetter> logger,
			LoggingLevelSwitch loggingLevelSwitch)
		{
			_logger = logger;
			_loggingLevelSwitch = loggingLevelSwitch;
		}

		public void SetVerbository(bool verboseOutput)
		{
			if (verboseOutput) _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
			else _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information; 
		}
	}
}
