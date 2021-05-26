using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Output.FileWriters
{
	public class CsvFile
	{
		public string FileLocation { get; set; } = string.Empty;

		public CsvLine HeadLine { get; set; } = new CsvLine();
		public List<CsvLine> BodyLines { get; set; } = new List<CsvLine>();
		public List<CsvLine> AllLines
		{
			get
			{
				List<CsvLine> lines = new List<CsvLine>();
				if (HeadLine != null) lines.Add(HeadLine);
				lines.AddRange(BodyLines);

				return lines;
			}
		}

		public int NumLines { get => BodyLines.Count + (HeadLine != null ? 1 : 0); }

		public CsvFile()
		{

		}

		private CsvFile(string fileLocation)
		{

		}

		public static CsvFile FromFileLocation(string fileLocation)
		{
			return new CsvFile(fileLocation);
		}
	}
}
