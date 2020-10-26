using FluentValidation;
using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Application.Articles.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
