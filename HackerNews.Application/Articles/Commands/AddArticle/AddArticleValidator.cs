using FluentValidation;
using HackerNews.Application.Articles.CommonValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Articles.Commands.AddArticle
{
	public class AddArticleValidator : AbstractValidator<AddArticleCommand>
	{
		public AddArticleValidator()
		{
			RuleFor(createReq => createReq.PostArticleModel).NotNull();
			RuleFor(createReq => createReq.PostArticleModel).SetValidator(new PostArticleModelValidator(), "*");
		}
	}
}
