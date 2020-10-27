using FluentValidation;
using HackerNews.Application.Common.Models.Articles;

namespace HackerNews.Application.Articles.CommonValidators
{
	public class PostArticleModelValidator : AbstractValidator<PostArticleModel>
	{
		public PostArticleModelValidator()
		{
			RuleFor(model => model).NotNull();
			RuleFor(model => model.Type).NotEmpty();
			RuleFor(model => model.BoardId).GreaterThan(0);
			RuleFor(model => model.Text).NotEmpty();
			RuleFor(model => model.Title).NotEmpty();
		}
	}
}
