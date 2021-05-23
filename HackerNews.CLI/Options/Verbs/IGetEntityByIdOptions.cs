using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Options.Verbs
{
	public interface IGetEntityByIdOptions :
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions,
		IIdOptions
	{
	}
}
