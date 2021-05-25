using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Loggers
{
	public class ConfigurableCommentLogger : ConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration>
	{
		public ConfigurableCommentLogger(ILogger<ConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration>> logger, IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel> articleInclusionReader, CommentInclusionConfiguration inclusionConfig) : base(logger, articleInclusionReader, inclusionConfig)
		{
		}

		protected override string GetEntityName()
			=> "Comment";

		protected override string GetEntityNamePlural()
			=> "Comments";
	}
}
