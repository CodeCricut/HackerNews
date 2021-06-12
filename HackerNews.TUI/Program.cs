using ConsoleFramework;
using ConsoleFramework.Controls;
using System;

namespace HackerNews.TUI
{
	class Program
	{
		public static void Main(string[] args)
		{
			var vm = new MainViewModel();

			var windowsHost = new WindowsHost();
			Window mainWindow = (Window)ConsoleApplication.LoadFromXaml("HackerNews.TUI.main.xml", vm);
			windowsHost.Show(mainWindow);
			ConsoleApplication.Instance.Run(windowsHost);
		}
	}
}
