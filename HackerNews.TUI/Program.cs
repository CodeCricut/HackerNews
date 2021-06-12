using ConsoleFramework;
using ConsoleFramework.Controls;
using System;

namespace HackerNews.TUI
{
	class Program
	{
		public static void Main(string[] args)
		{
			var windowsHost = new WindowsHost();
			
			var vm = new LoginWindowViewModel();
			var loginWindow = (Window)ConsoleApplication.LoadFromXaml("HackerNews.TUI.LoginWindow.xml", vm);
			
			windowsHost.Show(loginWindow);
			ConsoleApplication.Instance.Run(windowsHost);
		}
	}
}
