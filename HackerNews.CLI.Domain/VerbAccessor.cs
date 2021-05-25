using CommandLine;
using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HackerNews.CLI.Domain
{
	public interface IVerbAccessor
	{
		IEnumerable<Type> GetVerbTypes();
	}

	public class VerbAccessor : IVerbAccessor
	{
		public IEnumerable<Type> GetVerbTypes()
		{
			return Assembly.GetAssembly(typeof(IVerbOptions))
				.GetTypes()
				.Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
		}
	}
}
