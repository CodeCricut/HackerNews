using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Verbs
{
	public interface IGetEntitiesOptions :
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions,
		IIdListOptions,
		IPageOptions
	{
	}
}
