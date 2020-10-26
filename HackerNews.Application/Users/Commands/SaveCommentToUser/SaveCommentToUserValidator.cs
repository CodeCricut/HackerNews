using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Commands.SaveCommentToUser
{
	class SaveCommentToUserValidator : AbstractValidator<SaveCommentToUserCommand>
	{
		public SaveCommentToUserValidator()
		{
			RuleFor(command => command.CommentId).SetValidator(new IntIdValidator(), "*");
		}
	}
}
