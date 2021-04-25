using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public class LoginWindowViewModelFactory : ViewModelFactory<LoginWindowViewModel>
	{
		private readonly IEventAggregator _ea;
		private readonly LoginModelViewModel _loginModelVm;
		private readonly RegisterViewModel _registerVm;
		private readonly IViewManager _viewManager;

		public LoginWindowViewModelFactory(IEventAggregator ea,
			LoginModelViewModel loginModelVm,
			RegisterViewModel registerVm,
			IViewManager viewManager)
		{
			_ea = ea;
			_loginModelVm = loginModelVm;
			_registerVm = registerVm;
			_viewManager = viewManager;
		}

		public LoginWindowViewModel Create() => new LoginWindowViewModel(_ea, _loginModelVm, _registerVm, _viewManager);
	}
}
