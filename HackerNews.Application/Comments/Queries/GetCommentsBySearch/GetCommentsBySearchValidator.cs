using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Comments.Queries.GetCommentsBySearch
{
	public class GetCommentsBySearchValidator : AbstractValidator<GetCommentsBySearchQuery>
	{
		public GetCommentsBySearchValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator());
		}
	}
}
