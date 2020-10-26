using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Common.Validators
{
	class IntIdValidator : AbstractValidator<int>
	{
		public IntIdValidator()
		{
			RuleFor(id => id).GreaterThan(0);
		}
	}
}
