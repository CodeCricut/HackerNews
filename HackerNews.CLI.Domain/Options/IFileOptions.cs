﻿using CommandLine;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IFileOptions
	{
		[Option('f', "file", HelpText = "The location of a file, which if specified, the entities will be written to")]
		public string FileLocation { get; set; }
	}
}
