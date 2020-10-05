﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.DB_Helpers
{
	public class TaskHelper
	{
		/// <summary>
		/// For each item in <paramref name="items"/>, start a task and run the <paramref name="func"/> using 
		/// the item for the input, and adding the output to the output list.
		/// 
		/// Most importantly, all tasks will run concurrently.
		/// </summary>
		/// <param name="items"></param>
		/// <param name="func"></param>
		/// <returns></returns>
		public static async Task<IEnumerable<OutputT>> RunConcurrentFuncsAsync<InputT, OutputT>(IEnumerable<InputT> items, Func<InputT, OutputT> func)
		{
			List<Task<OutputT>> tasks = new List<Task<OutputT>>();

			foreach (var item in items)
				tasks.Add(Task.Factory.StartNew(() => func(item)));

			return (await Task.WhenAll(tasks)).ToList();
		}

		public static async Task<IEnumerable<OutputT>> RunConcurrentTasksAsync<InputT, OutputT>(IEnumerable<InputT> items, Func<InputT, Task<OutputT>> task)
		{
			List<Task<OutputT>> tasks = new List<Task<OutputT>>();

			foreach (var item in items)
				tasks.Add(task(item));

			return (await Task.WhenAll(tasks)).ToList();
		}
	}
}
