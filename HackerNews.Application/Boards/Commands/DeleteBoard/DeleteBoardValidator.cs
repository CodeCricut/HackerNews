using FluentValidation;

namespace HackerNews.Application.Boards.Commands.DeleteBoard
{
	class DeleteBoardValidator : AbstractValidator<DeleteBoardCommand>
	{
		public DeleteBoardValidator()
		{
			RuleFor(command => command).NotNull();
			RuleFor(command => command.Id).GreaterThan(0);
		}
	}
}
