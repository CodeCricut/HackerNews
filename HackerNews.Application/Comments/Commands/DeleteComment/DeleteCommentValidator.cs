using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Comments.Commands.DeleteComment
{
	class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
	{
		public DeleteCommentValidator()
		{
			RuleFor(command => command.Id).SetValidator(new IntIdValidator(), "*");
		}
	}
}
