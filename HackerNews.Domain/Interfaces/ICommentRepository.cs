using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface ICommentRepository : IEntityRepository<Comment>
	{
		Task<Comment> VoteComment(int id, bool upvote);
		Task<Comment> AddSubComment(int parentId, Comment comment);
	}
}
