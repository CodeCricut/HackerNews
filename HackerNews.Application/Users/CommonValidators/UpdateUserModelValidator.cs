using FluentValidation;
using HackerNews.Application.Common.Models.Users;

namespace HackerNews.Application.Users.CommonValidators
{
	class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
	{
		public UpdateUserModelValidator()
		{
			RuleFor(model => model.FirstName).NotEmpty();
			RuleFor(model => model.LastName).NotEmpty();
			RuleFor(model => model.Password).NotEmpty();
			RuleFor(model => model.Password.Length).GreaterThan(5);
		}
	}
}
