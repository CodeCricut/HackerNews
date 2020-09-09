using HackerNews.Api.Converters;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using HackerNews.EF.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class CommentHelper : EntityHelper<Comment, PostCommentModel, GetCommentModel>, IVoteableEntityHelper<Comment>
	{
		public CommentHelper(IEntityRepository<Comment> entityRepository, IEntityConverter<Comment, PostCommentModel, GetCommentModel> commentConverter) 
			: base(entityRepository, commentConverter)
		{
		}

		public override void UpdateEntityProperties(Comment comment, Comment newComment)
		{
			// this is messy, but a quick fix
			comment.AuthorName = newComment.AuthorName;
			comment.Text = newComment.Text;
		}

		public async Task VoteEntityAsync(int id, bool upvote)
		{
			var comment = await GetEntityAsync(id);
			comment.Karma = upvote
				? comment.Karma + 1
				: comment.Karma - 1;

			await _entityRepository.UpdateEntityAsync(id, comment);
			await _entityRepository.SaveChangesAsync();
		}

		public override async Task<GetCommentModel> GetEntityModelAsync(int id)
		{
			var comment = await GetEntityAsync(id);

			comment = Trimmer.GetNewTrimmedComment(comment, false, false);

			return await _entityConverter.ConvertEntityAsync<GetCommentModel>(comment);
		}

		public override async Task<List<Comment>> GetAllEntitiesAsync()
		{
			List<Comment> comments = (await _entityRepository.GetEntitiesAsync()).ToList();

			return await Trimmer.GetNewTrimmedCommentsAsync(comments, false, false);
		}
	}
}
