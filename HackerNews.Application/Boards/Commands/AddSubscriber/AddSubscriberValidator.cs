using FluentValidation;

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
