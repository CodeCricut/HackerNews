using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hackernews.WPF
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private LoginWindowViewModel ViewModel { get; }

		public LoginWindow(IServiceProvider serviceProvider)
		{
			InitializeComponent();

			ViewModel = new LoginWindowViewModel(serviceProvider, this);
			rootElement.DataContext = ViewModel;
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (ViewModel != null)
				ViewModel.Password = ((PasswordBox)sender).SecurePassword;
		}
	}
}
