using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Home
{
	public class HomeSearchViewModel
	{
		public PagedListResponse<GetBoardModel> Boards { get; set; }
		public PagedListResponse<GetPublicUserModel> Users { get; set; }
		public PagedListResponse<GetArticleModel> Articles { get; set; }
		public PagedListResponse<GetCommentModel> Comments { get; set; }
	}
}
