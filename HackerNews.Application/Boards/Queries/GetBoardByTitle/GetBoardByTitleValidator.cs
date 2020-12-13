using FluentValidation;

namespace HackerNews.Application.Boards.Queries.GetBoardByTitle
{
	public class GetBoardByTitleValidator : AbstractValidator<GetBoardByTitleQuery>
	{
		public GetBoardByTitleValidator()
		{
			RuleFor(query => query.Title).NotEmpty();
		}
	}
}
