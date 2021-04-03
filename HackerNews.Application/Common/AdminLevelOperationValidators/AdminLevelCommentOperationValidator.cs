using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.AdminLevelOperationValidators
{
	public class AdminLevelCommentOperationValidator : AdminLevelOperationValidator<Comment>
	{
		public AdminLevelCommentOperationValidator(IMediator mediator) : base(mediator)
		{
		}

		public override async Task<bool> CanDeleteEntityAsync(Comment entity, AdminLevel adminLevel)
		{
			switch (adminLevel)
			{
				case AdminLevel.Global:
					return true;
				case AdminLevel.Restricted:
					var user = await _mediator.Send(new GetAuthenticatedUserQuery());
					bool userModeratingCommentBoard = user.BoardsModerating.Contains(entity.BoardId);
					return userModeratingCommentBoard;
				case AdminLevel.None:
				default:
					return false;
			}
		}

		public override async Task<bool> CanModifyEntityAsync(Comment entity, AdminLevel adminLevel)
		{
			switch (adminLevel)
			{
				case AdminLevel.Global:
					return true;
				case AdminLevel.Restricted:
					var user = await _mediator.Send(new GetAuthenticatedUserQuery());
					bool userModeratingCommentBoard = user.BoardsModerating.Contains(entity.BoardId);
					return userModeratingCommentBoard;
				case AdminLevel.None:
				default:
					return false;
			}
		}
	}
}
