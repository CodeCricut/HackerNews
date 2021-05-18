using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Articles;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetArticleProcessor : IGetVerbProcessor<PostArticleModel, GetArticleModel>
	{

	}

	public class GetArticleProcessor : GetVerbProcessor<PostArticleModel, GetArticleModel>, IGetArticleProcessor
	{
		public GetArticleProcessor(IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient, IEntityLogger<GetArticleModel> entityLogger)
			: base(entityApiClient, entityLogger)
		{
		}
	}
}
