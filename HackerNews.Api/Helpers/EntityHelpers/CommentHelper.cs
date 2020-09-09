using HackerNews.Api.Converters;
using HackerNews.Api.Converters.Trimmers;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class CommentHelper : EntityHelper<Comment, PostCommentModel, GetCommentModel>, IVoteableEntityHelper<Comment>
	{
		public CommentHelper(EntityRepository<Comment> entityRepository, CommentConverter commentConverter) 
			: base(entityRepository, commentConverter)
		{
		}

		public override void UpdateEntityProperties(Comment comment, PostCommentModel commentModel)
		{
			// this is messy, but a quick fix
			comment.AuthorName = commentModel.AuthorName;
			comment.Text = commentModel.Text;
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

		public override async Task<List<GetCommentModel>> GetAllEntityModelsAsync()
		{
			var comments = await GetAllEntitiesAsync();
			return await _entityConverter.ConvertEntitiesAsync<GetCommentModel>(comments);
		}

		public override async Task<List<Comment>> GetAllEntitiesAsync()
		{
			List<Comment> comments = (await _entityRepository.GetEntitiesAsync()).ToList();

			return await Trimmer.GetNewTrimmedCommentsAsync(comments, false, false);
		}
	}
}
