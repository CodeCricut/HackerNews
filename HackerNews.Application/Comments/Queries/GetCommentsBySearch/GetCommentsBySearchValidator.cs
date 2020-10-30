using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Comments.Queries.GetCommentsBySearch
{
	public class GetCommentsBySearchValidator : AbstractValidator<GetCommentsBySearchQuery>
	{
		public GetCommentsBySearchValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator());
		}
	}
}
