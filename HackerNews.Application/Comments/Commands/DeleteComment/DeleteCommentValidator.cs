using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Comments.Commands.DeleteComment
{
	class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
	{
		public DeleteCommentValidator()
		{
			RuleFor(command => command.Id).SetValidator(new IntIdValidator(), "*");
		}
	}
}
