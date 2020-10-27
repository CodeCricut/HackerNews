using FluentValidation;
using HackerNews.Application.Articles.CommonValidators;

namespace HackerNews.Application.Articles.Commands.AddArticles
{
	public class AddArticlesValidator : AbstractValidator<AddArticlesCommand>
	{
		public AddArticlesValidator()
		{
			RuleFor(createReq => createReq.PostArticleModels).NotNull();
			RuleForEach(createReq => createReq.PostArticleModels).SetValidator(new PostArticleModelValidator(), "*");
		}
	}
}
