using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Output.FileWriters;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class PublicUserCsvWriter : EntityCsvWriter<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public PublicUserCsvWriter(ICsvFileWriter csvWriter, ILogger<EntityCsvWriter<GetPublicUserModel, PublicUserInclusionConfiguration>> logger, IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel> inclusionReader, PublicUserInclusionConfiguration inclusionConfig) : base(csvWriter, logger, inclusionReader, inclusionConfig)
		{
		}
	}
}
