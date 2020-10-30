using FluentValidation;
using HackerNews.Application.Boards.Queries.GetBoardsByIds;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Boards.Queries.GetBoardsBySearch
{
	public class GetBoardsBySearchValidator : AbstractValidator<GetBoardsByIdsQuery>
	{
		public GetBoardsBySearchValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
