using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class PublicUserCsvWriter : IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public void Configure(PublicUserInclusionConfiguration config)
		{
			throw new NotImplementedException();
		}

		public Task WriteEntityAsync(string fileLoc, GetPublicUserModel entity)
		{
			throw new NotImplementedException();
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetPublicUserModel> entityPage)
		{
			throw new NotImplementedException();
		}
	}
}
