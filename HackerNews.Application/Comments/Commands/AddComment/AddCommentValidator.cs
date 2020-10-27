using FluentValidation;
using HackerNews.Application.Comments.CommonValidators;

namespace HackerNews.Application.Comments.Commands.AddComment
{
	class AddCommentValidator : AbstractValidator<AddCommentCommand>
	{
		public AddCommentValidator()
		{
			RuleFor(command => command.PostCommentModel).SetValidator(new PostCommentModelValidator(), "*");
		}
	}
}
