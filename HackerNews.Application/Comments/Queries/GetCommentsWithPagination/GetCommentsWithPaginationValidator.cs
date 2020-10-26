using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Comments.Queries.GetCommentsWithPagination
{
	class GetCommentsWithPaginationValidator : AbstractValidator<GetCommentsWithPaginationQuery>
	{
		public GetCommentsWithPaginationValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
