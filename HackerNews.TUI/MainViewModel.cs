using ConsoleFramework.Events;
using HackerNews.WPF.Core.ViewModel;

namespace HackerNews.TUI
{
	public class MainViewModel : BaseViewModel
	{
		public ICommand ChangeTextCommand { get; init; }

		private string _myText;

		public string MyText
		{
			get { return _myText; }
			set { Set(ref _myText, value); }
		}

		public MainViewModel()
		{
			MyText = "Starting text";
			ChangeTextCommand = new RelayCommand(
				_ => MyText = "new text");
		}
	}
}
