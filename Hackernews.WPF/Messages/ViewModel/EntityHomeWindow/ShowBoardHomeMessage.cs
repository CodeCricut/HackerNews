using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.WPF.MessageBus.ViewModel.EntityHomeWindow
{
	public sealed class ShowBoardHomeMessage
	{
		public ShowBoardHomeMessage(BoardViewModel boardVM)
		{
			BoardVM = boardVM;
		}

		public BoardViewModel BoardVM { get; }
	}
}
