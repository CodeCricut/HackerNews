using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.DataAccess.EntityFinders;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class CommentFinder : EntityFinder<GetCommentModel, PostCommentModel>
	{
		public CommentFinder(IEntityApiClient<PostCommentModel, GetCommentModel> entityApiClient, ILogger<EntityFinder<GetCommentModel, PostCommentModel>> logger) : base(entityApiClient, logger)
		{
		}
	}
}
