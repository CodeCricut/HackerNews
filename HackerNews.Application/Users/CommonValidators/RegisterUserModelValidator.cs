using FluentValidation;
using HackerNews.Domain.Models.Users;

namespace HackerNews.Application.Users.CommonValidators
{
	class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
	{
		public RegisterUserModelValidator()
		{
			RuleFor(model => model.FirstName).NotEmpty();
			RuleFor(model => model.LastName).NotEmpty();
			RuleFor(model => model.Username).NotEmpty();
			RuleFor(model => model.Username.Length).GreaterThan(3);
			RuleFor(model => model.Password).NotEmpty();
			RuleFor(model => model.Password.Length).GreaterThan(5);
		}
	}
}
