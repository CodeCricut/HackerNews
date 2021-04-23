using Hackernews.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Core
{
	/// <summary>
	/// Views which inherit from this interface have the view model <typeparamref name="TViewModel"/>.
	/// </summary>
	/// <typeparam name="TViewModel"></typeparam>
	public interface IHaveViewModel<TViewModel> where TViewModel : BaseViewModel
	{
	}
}
