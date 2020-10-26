using FluentValidation;
using HackerNews.Application.Users.CommonValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Commands.UpdateUser
{
	class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserValidator()
		{
			RuleFor(command => command.UpdateUserModel).SetValidator(new UpdateUserModelValidator(), "*");
		}
	}
}
