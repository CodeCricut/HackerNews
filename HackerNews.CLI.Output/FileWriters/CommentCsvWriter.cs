using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Output.FileWriters;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class CommentCsvWriter : EntityCsvWriter<GetCommentModel, CommentInclusionConfiguration>
	{
		public CommentCsvWriter(ICsvFileWriter csvWriter, ILogger<EntityCsvWriter<GetCommentModel, CommentInclusionConfiguration>> logger, IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel> inclusionReader, CommentInclusionConfiguration inclusionConfig) : base(csvWriter, logger, inclusionReader, inclusionConfig)
		{
		}
	}
}
