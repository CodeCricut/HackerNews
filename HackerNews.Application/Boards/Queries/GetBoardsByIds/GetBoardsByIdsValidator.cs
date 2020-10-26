using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Application.Boards.Queries.GetBoardsByIds
{
	class GetBoardsByIdsValidator : AbstractValidator<GetBoardsByIdsQuery>
	{
		public GetBoardsByIdsValidator()
		{
			RuleForEach(query => query.Ids).SetValidator(new IntIdValidator(), "*");
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
