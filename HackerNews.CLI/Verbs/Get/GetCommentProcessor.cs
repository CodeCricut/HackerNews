using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Comments;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetCommentProcessor : IGetVerbProcessor<PostCommentModel, GetCommentModel>
	{

	}

	public class GetCommentProcessor : GetVerbProcessor<PostCommentModel, GetCommentModel>, IGetCommentProcessor
	{
		public GetCommentProcessor(IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient, IEntityLogger<GetCommentModel> entityLogger, IEntityWriter<GetCommentModel> entityWriter) : base(entityApiClient, entityLogger, entityWriter)
		{
		}
	}
}
