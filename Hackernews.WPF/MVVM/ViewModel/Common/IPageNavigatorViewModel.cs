using Hackernews.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public interface IPageNavigatorViewModel
	{
		public int CurrentPage { get; }
		public int TotalPages { get;  }

		AsyncDelegateCommand PrevPageCommand { get;  }
		AsyncDelegateCommand NextPageCommand { get; }
	}
}
