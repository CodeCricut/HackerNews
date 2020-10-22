using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HackerNews.ViewModels.Users
{
	public class UserCommentsListView 
	{
		public Page<GetCommentModel> CommentPage { get; set; }
	}
}
