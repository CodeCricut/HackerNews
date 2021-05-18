using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public interface IFileWriter
	{
		Task<bool> WriteToFileAsync(string fileLoc, IEnumerable<string> lines);
	}

	public class FileWriter : IFileWriter
	{
		public async Task<bool> WriteToFileAsync(string fileLoc, IEnumerable<string> lines)
		{
			if (!System.IO.File.Exists(fileLoc)) 
				return false;

			using System.IO.StreamWriter file = new System.IO.StreamWriter(fileLoc);
			try
			{
				foreach (string line in lines)
				{
					await file.WriteLineAsync(line);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
