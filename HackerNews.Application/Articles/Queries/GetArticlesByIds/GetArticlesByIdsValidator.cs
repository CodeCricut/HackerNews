using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Articles.Queries.GetArticlesByIds
{
	class GetArticlesByIdsValidator : AbstractValidator<GetArticlesByIdsQuery>
	{
		public GetArticlesByIdsValidator()
		{
			RuleForEach(query => query.Ids).SetValidator(new IntIdValidator(), "*");
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
