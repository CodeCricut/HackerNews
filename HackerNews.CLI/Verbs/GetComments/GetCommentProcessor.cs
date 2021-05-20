using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Comments;

namespace HackerNews.CLI.Verbs.GetComments
{
	public interface IGetCommentProcessor : IGetVerbProcessor<GetCommentModel, GetCommentsOptions>
	{

	}

	public class GetCommentProcessor : GetVerbProcessor<GetCommentModel, GetCommentsOptions>, IGetCommentProcessor
	{
		private readonly IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> _configEntityWriter;

		public GetCommentProcessor(IGetEntityRepository<GetCommentModel> entityRepository,
			IEntityLogger<GetCommentModel> entityLogger,
			IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetCommentsOptions options)
		{
			_configEntityWriter.Configure(options.GetCommentInclusionConfiguration());
		}
	}
}
