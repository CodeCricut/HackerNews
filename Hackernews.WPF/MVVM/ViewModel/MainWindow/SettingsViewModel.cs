using HackerNews.WPF.Core;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;

		public bool IsDarkTheme
		{
			get => App.Skin == Skin.Dark;
		}


		public ICommand ToggleSkinCommand { get; }

		public SettingsViewModel(IEventAggregator ea)
		{
			ToggleSkinCommand = new DelegateCommand(ToggleSkin);
			_ea = ea;
		}

		private void ToggleSkin(object darkToggled)
		{
			if ((bool)darkToggled)
				_ea.SendMessage(new ChangeSkinMessage(Skin.Dark));
			else
				_ea.SendMessage(new ChangeSkinMessage(Skin.Light));

			RaisePropertyChanged(nameof(IsDarkTheme));
		}
	}
}
