using FluentValidation;
using HackerNews.Application.Articles.CommonValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Articles.Commands.UpdateArticle
{
	public class UpdateArticleValidator : AbstractValidator<UpdateArticleCommand>
	{
		public UpdateArticleValidator()
		{
			RuleFor(command => command.PostArticleModel).SetValidator(new PostArticleModelValidator(), "*");
		}
	}
}
