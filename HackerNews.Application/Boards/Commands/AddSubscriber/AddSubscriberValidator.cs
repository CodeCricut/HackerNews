using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Boards.Commands.AddSubscriber
{
	class AddSubscriberValidator : AbstractValidator<AddSubscriberCommand>
	{
		public AddSubscriberValidator()
		{
			RuleFor(command => command).NotNull();
			RuleFor(command => command.BoardId).GreaterThan(0);
		}
	}
}
