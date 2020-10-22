using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Home
{
	public class HomeIndexViewModel
	{
		public Page<GetArticleModel> ArticlePage { get; set; }
		public string SearchTerm { get; set; }
	}
}
