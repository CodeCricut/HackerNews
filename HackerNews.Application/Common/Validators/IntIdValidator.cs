using FluentValidation;

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
