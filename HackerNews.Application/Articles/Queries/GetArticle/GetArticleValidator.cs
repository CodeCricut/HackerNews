using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Articles.Queries.GetArticle
{
	class GetArticleValidator : AbstractValidator<GetArticleQuery>
	{
		public GetArticleValidator()
		{
			RuleFor(query => query.Id).SetValidator(new IntIdValidator(), "*");
		}
	}
}
