using HackerNews.Application.Common.Models.Articles;
using HackerNews.Blazor.Client.Services;
using HackerNews.Domain.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Blazor.Client.Pages
{
	public partial class Index
	{
		[Inject]
		public IArticleApiService ArticleApiService { get; set; }

		[Parameter]
		public PagingParams PagingParams { get; set; } = new PagingParams();

		public PaginatedList<GetArticleModel> ArticlePage { get; set; } = new PaginatedList<GetArticleModel>(new List<GetArticleModel>(), 0, 1, 0);

		protected override async Task OnInitializedAsync()
		{
			ArticlePage = await ArticleApiService.GetArticlesAsync(PagingParams);
		}
	}
}
