using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Commands.SaveArticleToUser
{
	class SaveArticleToUserValidator : AbstractValidator<SaveArticleToUserCommand>
	{
		public SaveArticleToUserValidator()
		{
			RuleFor(command => command.ArticleId).SetValidator(new IntIdValidator(), "*");
		}
	}
}
