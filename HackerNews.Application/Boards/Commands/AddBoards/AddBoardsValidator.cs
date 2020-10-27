using FluentValidation;
using HackerNews.Application.Boards.CommonValidators;

namespace HackerNews.Application.Boards.Commands.AddBoards
{
	class AddBoardsValidator : AbstractValidator<AddBoardsCommand>
	{
		public AddBoardsValidator()
		{
			RuleFor(command => command).NotNull();
			RuleForEach(command => command.PostBoardModels).SetValidator(new PostBoardModelValidator(), "*");
		}
	}
}
