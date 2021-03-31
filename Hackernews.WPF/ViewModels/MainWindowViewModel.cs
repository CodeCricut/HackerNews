﻿using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public ArticlesViewModel ArticleViewModel { get; set; }

		public MainWindowViewModel(IMediator mediator)
		{
			ArticleViewModel = new ArticlesViewModel(new GetArticleModel(), mediator);
		}
	}
}
