using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.ViewModels.Articles
{
	public class ArticleCreateViewModel 
	{
		public PostArticleModel Article { get; set; }
		public IEnumerable<SelectListItem> ArticleTypeList { get
			{
				return Enum.GetNames(typeof(ArticleType)).Select(name => new SelectListItem() { Text = name, Value = name });
			}
		}
	}
}
