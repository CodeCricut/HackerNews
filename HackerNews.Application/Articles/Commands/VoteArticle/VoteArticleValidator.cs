using FluentValidation;

namespace HackerNews.Application.Articles.Commands.VoteArticle
{
	class VoteArticleValidator : AbstractValidator<VoteArticleCommand>
	{
		public VoteArticleValidator()
		{
			RuleFor(command => command.ArticleId).GreaterThan(0);
		}
	}
}
