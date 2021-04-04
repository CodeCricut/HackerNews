using Hackernews.WPF.Helpers;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	class LoginWindowViewModel : BaseViewModel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly Window _thisWindow;

		private string _username;
		public string Username 
		{
			get => _username;
			set
			{
				if (_username != value)
				{
					_username = value;
					RaisePropertyChanged();
					LoginCommand.RaiseCanExecuteChanged();
				}
			}
		}

		private SecureString _password;
		public SecureString Password
		{
			private get => _password;
			set
			{
				if (_password != value)
				{
					_password = value;
					RaisePropertyChanged();
					LoginCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public AsyncDelegateCommand LoginCommand { get; set; }
		public LoginWindowViewModel(IServiceProvider serviceProvider, Window thisWindow)
		{
			LoginCommand = new AsyncDelegateCommand(LoginAsync, CanLogin);
			_serviceProvider = serviceProvider;
			_thisWindow = thisWindow;
		}

		private async Task LoginAsync()
		{
			//// lol wut is sexurity?
			//string password = new System.Net.NetworkCredential(string.Empty, _password).Password;
			//var loginModel = new LoginModel() { UserName = _username, Password = password };

			//await _signInManager.PasswordSignInAsync(Username, password, isPersistent: false, lockoutOnFailure: false);

			//Window mainWindow = new MainWindow(_mediator);
			//mainWindow.Show();

			//_thisWindow.Close();
			throw new NotImplementedException();
		}

		public bool CanLogin() => !(string.IsNullOrEmpty(Username) || Password?.Length <= 0);  
	}
}
