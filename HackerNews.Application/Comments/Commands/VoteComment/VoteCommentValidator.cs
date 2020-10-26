using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Comments.Commands.VoteComment
{
	class VoteCommentValidator : AbstractValidator<VoteCommentCommand>
	{
		public VoteCommentValidator()
		{
			RuleFor(command => command.CommentId).SetValidator(new IntIdValidator(), "*");
		}
	}
}
