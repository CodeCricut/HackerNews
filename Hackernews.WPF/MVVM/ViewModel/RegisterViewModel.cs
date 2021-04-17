using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
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
		#endregion

		private readonly LoginWindowViewModel _loginWindowVM;
		private readonly ISignInManager _signInManager;
		private readonly IApiClient _apiClient;

		public RegisterViewModel(LoginWindowViewModel loginWindowVm, ISignInManager signInManager, IApiClient apiClient)
		{
			_loginWindowVM = loginWindowVm;
			_signInManager = signInManager;
			_apiClient = apiClient;

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
			_loginWindowVM.Loading = true;
			_loginWindowVM.InvalidUserInput = false;

			try
			{
				Jwt jwt = await _apiClient.PostAsync<RegisterUserModel, Jwt>(_registerModel, "account/register");

				var loginModel = new LoginModel
				{
					UserName = Username, Password = Password
				};
				await _signInManager.SignInAsync(loginModel);

				_loginWindowVM.SwitchToMainWindow();
			}
			catch (Exception e)
			{
				RegisterModel = new RegisterUserModel();

				_loginWindowVM.InvalidUserInput = true;
			}
			finally
			{
				_loginWindowVM.Loading = false;
			}
		}
	}
}
