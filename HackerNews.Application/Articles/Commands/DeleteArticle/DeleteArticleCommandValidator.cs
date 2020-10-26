using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Articles.Commands.DeleteArticle
{
	public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
	{
		public DeleteArticleCommandValidator()
		{
			RuleFor(command => command.Id).GreaterThan(0);
		}
	}
}
