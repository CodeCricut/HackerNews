using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Comments;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetCommentProcessor : IGetVerbProcessor<GetCommentModel>
	{

	}

	public class GetCommentProcessor : GetVerbProcessor<GetCommentModel>, IGetCommentProcessor
	{
		private readonly IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> _configEntityWriter;

		public GetCommentProcessor(IGetEntityRepository<GetCommentModel> entityRepository, 
			IEntityLogger<GetCommentModel> entityLogger, 
			IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> entityWriter) 
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetVerbOptions options)
		{
			_configEntityWriter.Configure(options.GetCommentInclusionConfiguration());
		}
	}
}
