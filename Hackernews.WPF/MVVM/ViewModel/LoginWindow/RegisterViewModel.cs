using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using HackerNews.WPF.MessageBus.Messages.ViewModel.LoginWindow;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class RegisterViewModel : BaseViewModel
	{
		private RegisterUserModel _registerModel = new RegisterUserModel();
		public RegisterUserModel RegisterModel
		{
			get => _registerModel;
			set
			{
				_registerModel = value;
				RaisePropertyChanged("");
				RegisterCommand.RaiseCanExecuteChanged();
			}
		}

		#region Register inputs
		public string Username
		{
			get => _registerModel.UserName; set
			{
				_registerModel.UserName = value;
				RaisePropertyChanged();
				RegisterCommand.RaiseCanExecuteChanged();
			}
		}

		public string Password
		{
			get => _registerModel.Password; set
			{
				_registerModel.Password = value;
				RaisePropertyChanged();
				RegisterCommand.RaiseCanExecuteChanged();
			}
		}

		public string Firstname
		{
			get => _registerModel.FirstName; set
			{
				_registerModel.FirstName = value;
				RaisePropertyChanged();
				RegisterCommand.RaiseCanExecuteChanged();
			}
		}

		public string Lastname
		{
			get => _registerModel.LastName; set
			{
				_registerModel.LastName = value;
				RaisePropertyChanged();
				RegisterCommand.RaiseCanExecuteChanged();
			}
		}

		private readonly IEventAggregator _ea;
		#endregion

		private readonly ISignInManager _signInManager;
		private readonly IRegistrationApiClient _registrationClient;

		public RegisterViewModel(IEventAggregator ea, ISignInManager signInManager, IRegistrationApiClient registrationClient)
		{
			_ea = ea;
			_signInManager = signInManager;
			_registrationClient = registrationClient;

			RegisterCommand = new AsyncDelegateCommand(RegisterAsync, CanRegister);
		}

		public AsyncDelegateCommand RegisterCommand { get; }
		public bool CanRegister(object parameter = null) =>
			Username?.Length > 0 &&
			Password?.Length > 0 &&
			Firstname?.Length > 0 &&
			Lastname?.Length > 0;

		private async Task RegisterAsync(object parameter = null)
		{
			_ea.SendMessage(new LoginWindowLoadingChangedMessage(isLoading: true));
			_ea.SendMessage(new LoginWindowInvalidUserInputChanged(invalidUserInput: false));

			try
			{
				Jwt jwt = await _registrationClient.RegisterAsync(RegisterModel);

				var loginModel = new LoginModel
				{
					UserName = Username,
					Password = Password
				};
				await _signInManager.SignInAsync(loginModel);

				_ea.SendMessage(new LoginWindowSwitchToMainWindowMessage());
			}
			catch (Exception)
			{
				RegisterModel = new RegisterUserModel();

				_ea.SendMessage(new LoginWindowInvalidUserInputChanged(invalidUserInput: true));
			}
			finally
			{
				_ea.SendMessage(new LoginWindowLoadingChangedMessage(isLoading: false));
			}
		}
	}
}
