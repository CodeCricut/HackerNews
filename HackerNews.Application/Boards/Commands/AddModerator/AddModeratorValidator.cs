using FluentValidation;

namespace HackerNews.Application.Boards.Commands.AddModerator
{
	class AddModeratorValidator : AbstractValidator<AddModeratorCommand>
	{
		public AddModeratorValidator()
		{
			RuleFor(command => command).NotNull();
			RuleFor(command => command.BoardId).GreaterThan(0);
			RuleFor(command => command.ModeratorId).GreaterThan(0);
		}
	}
}
