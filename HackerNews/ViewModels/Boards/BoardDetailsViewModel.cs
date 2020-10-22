using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers;
using HackerNews.ViewModels.Base;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Boards
{
	public class BoardDetailsViewModel : DetailsViewModel<GetBoardModel>
	{


		public BoardDetailsViewModel(PagedListResponse<GetArticleModel> pagedListResponse)
		{
			Articles = pagedListResponse;
		}

		public PagedListResponse<GetArticleModel> Articles { get; set; }
		public PagingParams PrevPagingParams { get => Articles.GetPrevPagingParams(); }
		public PagingParams NextPagingParams { get => Articles.GetNextPagingParams(); }
		public IEnumerable<GetPublicUserModel> Moderators { get; set; }
	}
}
