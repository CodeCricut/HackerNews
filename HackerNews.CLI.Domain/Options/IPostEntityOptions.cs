using HackerNews.CLI.Options;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Options
{
	public interface IPostEntityOptions :
		ILoginOptions,
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions
	{
	}
}
