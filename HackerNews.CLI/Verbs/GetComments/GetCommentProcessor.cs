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
		private readonly IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> _configEntityLogger;
		private readonly IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> _configEntityWriter;

		public GetCommentProcessor(IGetEntityRepository<GetCommentModel> entityRepository,
			IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityLogger = entityLogger;
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetCommentsOptions options)
		{
			var config = options.GetCommentInclusionConfiguration();
			_configEntityLogger.Configure(config);
			_configEntityWriter.Configure(config);
		}
	}
}
