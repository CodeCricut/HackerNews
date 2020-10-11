using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.EF.Repositories.Comments
{
	public class WriteCommentRepository : WriteEntityRepository<Comment>
	{
		public WriteCommentRepository(DbContext context) : base(context)
		{
		}
	}
}
