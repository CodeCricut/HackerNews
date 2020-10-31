using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleCreateViewModel
	{
		public PostArticleModel Article { get; set; }
		public IEnumerable<SelectListItem> ArticleTypeList
		{
			get
			{
				return Enum.GetNames(typeof(ArticleType)).Select(name => new SelectListItem() { Text = name, Value = name });
			}
		}
	}
}
