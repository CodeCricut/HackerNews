using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Messages.ViewModel.EntityHomeWindow
{
	public sealed class ShowArticleHomeMessage
	{
		public ShowArticleHomeMessage(ArticleViewModel articleVm)
		{
			ArticleVm = articleVm;
		}

		public ArticleViewModel ArticleVm { get; }
	}
}
