using FluentValidation;
using HackerNews.Application.Users.CommonValidators;

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
