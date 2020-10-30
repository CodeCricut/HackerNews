using FluentValidation;
using HackerNews.Application.Common.Validators;
using HackerNews.Application.Users.Queries.GetUsersBySearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Queries.GetCommentsBySearch
{
	public class GetUsersBySearchValidator : AbstractValidator<GetUsersBySearchQuery>
	{
		public GetUsersBySearchValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator());
		}
	}
}
