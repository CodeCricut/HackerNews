using FluentValidation;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Queries.GetPublicUsersByIds
{
	class GetPublicUserByIdsValidator : AbstractValidator<GetPublicUsersByIdsQuery>
	{
		public GetPublicUserByIdsValidator()
		{
			RuleForEach(query => query.Ids).SetValidator(new IntIdValidator(), "*");
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
