using HackerNews.Domain.Common.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Articles
{
	public class ArticleAddImageViewModel
	{
		public int ArticleId { get; set; }
		public PostImageModel PostImageModel { get; set; }
	}
}
