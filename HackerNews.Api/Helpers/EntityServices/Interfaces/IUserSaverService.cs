using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public interface IUserSaverService
	{
		Task<GetPrivateUserModel> SaveArticleToUserAsync(User currentUser, int articleId);
		Task<GetPrivateUserModel> SaveCommentToUserAsync(User currentUser, int commentId);
	}
}
