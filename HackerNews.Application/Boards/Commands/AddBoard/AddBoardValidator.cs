using FluentValidation;
using HackerNews.Application.Boards.CommonValidators;

namespace HackerNews.Application.Boards.Commands.AddBoard
{
	class AddBoardValidator : AbstractValidator<AddBoardCommand>
	{
		public AddBoardValidator()
		{
			RuleFor(command => command.PostBoardModel).SetValidator(new PostBoardModelValidator(), "*");
		}
	}
}
