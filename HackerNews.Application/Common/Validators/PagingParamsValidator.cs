using FluentValidation;
using HackerNews.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Common.Validators
{
	class PagingParamsValidator : AbstractValidator<PagingParams>
	{
		public PagingParamsValidator()
		{
			RuleFor(pagingParams => pagingParams.PageNumber).GreaterThan(0);
			RuleFor(pagingParams => pagingParams.PageSize).LessThan(50);
		}
	}
}
