using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.AdminLevelOperationValidators
{
	public class AdminLevelArticleOperationValidator : AdminLevelOperationValidator<Article>
	{
		public AdminLevelArticleOperationValidator(IMediator mediator) : base(mediator)
		{
		}

		public override async Task<bool> CanDeleteEntityAsync(Article entity, AdminLevel adminLevel)
		{
			switch (adminLevel)
			{
				case AdminLevel.Global:
					return true;
				case AdminLevel.Restricted:
					var user = await _mediator.Send(new GetAuthenticatedUserQuery());
					bool userModeratingArticleBoard = user.BoardsModerating.Contains(entity.BoardId);
					return userModeratingArticleBoard;
				case AdminLevel.None:
				default:
					return false;
			}
		}

		public override async Task<bool> CanModifyEntityAsync(Article entity, AdminLevel adminLevel)
		{
			switch (adminLevel)
			{
				case AdminLevel.Global:
					return true;
				case AdminLevel.Restricted:
					var user = await _mediator.Send(new GetAuthenticatedUserQuery());
					bool userModeratingArticleBoard = user.BoardsModerating.Contains(entity.BoardId);
					return userModeratingArticleBoard;
				case AdminLevel.None:
				default:
					return false;
			}
		}
	}
}
