using HackerNews.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Helpers
{
	public static class ExceptionUtilities
	{
		public static void ExecuteAndCatchException(Action tryAction)
		{
			try
			{
				tryAction.Invoke();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public static void ExecuteAndCatchException(Action tryAction, Action catchAction)
		{
			try
			{
				tryAction.Invoke();
			}
			catch (Exception ex)
			{
				catchAction.Invoke();
			}
		}
	}
}
