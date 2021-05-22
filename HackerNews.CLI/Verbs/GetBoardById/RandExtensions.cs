using HackerNews.CLI.Configuration;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	public static class VerbosityOptionsExtensions
	{
		public static void WithVerbosity(this IVerbosityOptions options, Action<bool> verboseCallback)
		{
			verboseCallback(options.Verbose);
		}

		public static void IfVerbose(this IVerbosityOptions options, Action verboseCallback)
		{
			if (options.Verbose) verboseCallback();
		}

		public static void IfNotVerbose(this IVerbosityOptions options, Action verboseCallback)
		{
			if (! options.Verbose) verboseCallback();
		}

		public static bool GetVerbosity(this IVerbosityOptions options)
		{
			return options.Verbose;
		}
	}

	public static class BoardInclusionOptionsExtensions
	{
		public static void WithInclusionConfiguration(this IBoardInclusionOptions options, Action<BoardInclusionConfiguration> inclusionConfigAction)
		{
			// TODO; get inclusion config from optoins
			BoardInclusionConfiguration config = new BoardInclusionConfiguration();
			inclusionConfigAction(config);
		}

		public static BoardInclusionConfiguration GetInclusionConfiguration(this IBoardInclusionOptions options)
		{
			// TODO; get inclusion config from optoins
			BoardInclusionConfiguration config = new BoardInclusionConfiguration();
			return config;
		}
	}

	public static class IdOptionsExtensions
	{
		public static void WithId(this IIdOptions options, Action<int> idAction)
		{
			int id = options.Id;
			idAction(id);
		}

		public static int GetId(this IIdOptions options)
		{
			return options.Id;
		}
	}

	public static class IPrintOptionsExtensions
	{
		public static void IfPrint(this IPrintOptions options, Action printAction)
		{
			if (options.Print) printAction();
		}

		public static bool GetPrint(this IPrintOptions options, Action printAction)
		{
			return options.Print;
		}
	}

	public static class IFileOptionsExtensions
	{
		public static void IfFileLocationProvided(this IFileOptions options, Action<string> fileAction)
		{
			if (!string.IsNullOrEmpty(options.FileLocation)) fileAction(options.FileLocation);
		}

		public static bool FileLocationWasProvided(this IFileOptions options)
		{
			return !string.IsNullOrEmpty(options.FileLocation);
		}

		public static string GetFileLocationProvided(this IFileOptions options)
		{
			return options.FileLocation;
		}
	}
}
