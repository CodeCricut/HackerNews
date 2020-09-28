using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using HackerNews.EF.Repositories;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class CommentHelper : EntityHelper<Comment, PostCommentModel, GetCommentModel>, IVoteableEntityHelper<Comment>
	{
		public CommentHelper(IEntityRepository<Comment> entityRepository, IMapper mapper)
			: base(entityRepository, mapper)
		{
		}

		public async Task VoteEntityAsync(int id, bool upvote)
		{
			var comment = await _entityRepository.GetEntityAsync(id);
			// GetEntityAsync(id);
			comment.Karma = upvote
				? comment.Karma + 1
				: comment.Karma - 1;

			await _entityRepository.UpdateEntityAsync(id, comment);
			await _entityRepository.SaveChangesAsync();
		}

		public override async Task<GetCommentModel> GetEntityModelAsync(int id)
		{
			var comment = await _entityRepository.GetEntityAsync(id);
			// GetEntityAsync(id);

			// comment = Trimmer.GetNewTrimmedComment(comment, false, false);

			return _mapper.Map<GetCommentModel>(comment);
			// await _entityConverter.ConvertEntityAsync<GetCommentModel>(comment);
		}
	}
}
