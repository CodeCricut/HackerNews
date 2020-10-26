using FluentValidation;
using HackerNews.Application.Users.CommonValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Commands.RegisterUser
{
	class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
	{
		public RegisterUserValidator()
		{
			RuleFor(command => command.RegisterUserModel).SetValidator(new RegisterUserModelValidator(), "*");
		}
	}
}
