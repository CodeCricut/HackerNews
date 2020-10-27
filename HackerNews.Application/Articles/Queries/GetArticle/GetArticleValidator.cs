using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Articles.Queries.GetArticle
{
	class GetArticleValidator : AbstractValidator<GetArticleQuery>
	{
		public GetArticleValidator()
		{
			RuleFor(query => query.Id).SetValidator(new IntIdValidator(), "*");
		}
	}
}
