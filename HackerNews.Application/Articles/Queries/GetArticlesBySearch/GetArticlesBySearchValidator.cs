using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Articles.Queries.GetArticlesBySearch
{
	public class GetArticlesBySearchValidator : AbstractValidator<GetArticlesBySearchQuery>
	{
		public GetArticlesBySearchValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator());
		}
	}
}
