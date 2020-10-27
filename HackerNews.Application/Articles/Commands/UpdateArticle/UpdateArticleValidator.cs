using FluentValidation;
using HackerNews.Application.Articles.CommonValidators;

namespace HackerNews.Application.Articles.Commands.UpdateArticle
{
	public class UpdateArticleValidator : AbstractValidator<UpdateArticleCommand>
	{
		public UpdateArticleValidator()
		{
			RuleFor(command => command.PostArticleModel).SetValidator(new PostArticleModelValidator(), "*");
		}
	}
}
