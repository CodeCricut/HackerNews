using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IWriteArticleController
	{
		Task<ActionResult<GetArticleModel>> AddComment(int articleId, PostCommentModel postCommentModel);
	}
}
