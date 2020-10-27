using FluentValidation;
using HackerNews.Application.Comments.CommonValidators;
using System.Linq;

namespace HackerNews.Application.Comments.Commands.AddComments
{
	class AddCommentsValidator : AbstractValidator<AddCommentsCommand>
	{
		public AddCommentsValidator()
		{
			RuleFor(command => command.PostCommentModels.Count()).LessThan(10);
			RuleForEach(command => command.PostCommentModels).SetValidator(new PostCommentModelValidator(), "*");
		}
	}
}
