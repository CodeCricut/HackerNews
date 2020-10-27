using FluentValidation;
using HackerNews.Application.Common.Models.Users;

namespace HackerNews.Application.Users.CommonValidators
{
	class LoginModelValidator : AbstractValidator<LoginModel>
	{
		public LoginModelValidator()
		{
			RuleFor(model => model.Username).NotEmpty();
			RuleFor(model => model.Password).NotEmpty();
		}
	}
}
