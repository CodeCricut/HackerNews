using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Comments.Commands.VoteComment
{
	class VoteCommentValidator : AbstractValidator<VoteCommentCommand>
	{
		public VoteCommentValidator()
		{
			RuleFor(command => command.CommentId).SetValidator(new IntIdValidator(), "*");
		}
	}
}
