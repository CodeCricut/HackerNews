using FluentValidation;
using HackerNews.Application.Boards.CommonValidators;

namespace HackerNews.Application.Boards.Commands.UpdateBoard
{
	class UpdateBoardValidator : AbstractValidator<UpdateBoardCommand>
	{
		public UpdateBoardValidator()
		{
			RuleFor(command => command).NotNull();
			RuleFor(command => command.BoardId).GreaterThan(0);
			RuleFor(command => command.PostBoardModel).SetValidator(new PostBoardModelValidator(), "*");
		}
	}
}
