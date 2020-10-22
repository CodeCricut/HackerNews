using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers;
using System.Collections.Generic;

namespace HackerNews.ViewModels.Boards
{
	public class BoardDetailsViewModel 
	{
		public GetBoardModel Board { get; set; }
		public Page<GetArticleModel> ArticlePage { get; set; }
		public Page<GetPublicUserModel> Moderators { get; set; }
	}
}
