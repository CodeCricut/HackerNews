using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.SaveCommentToUser
{
	public class SaveCommentToUserCommand : IRequest<GetPrivateUserModel>
	{

		public SaveCommentToUserCommand(int commentId)
		{
			CommentId = commentId;
		}

		public int CommentId { get; }
	}

	public class SaveCommentToUserHandler : DatabaseRequestHandler<SaveCommentToUserCommand, GetPrivateUserModel>
	{
		public SaveCommentToUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetPrivateUserModel> Handle(SaveCommentToUserCommand request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;
			var user = await UnitOfWork.Users.GetEntityAsync(userId);
			if (user == null) throw new UnauthorizedException();

			var comment = await UnitOfWork.Comments.GetEntityAsync(request.CommentId);
			if (comment == null) throw new NotFoundException();

			bool alreadySaved = comment.UsersSaved.Where(us => us.UserId == user.Id).Count() > 0;

			// Remove if already saved.
			if (alreadySaved)
			{
				var joinEntity = comment.UsersSaved.FirstOrDefault(us => us.UserId == user.Id);
				comment.UsersSaved.Remove(joinEntity);
				user.SavedComments.Remove(joinEntity);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetPrivateUserModel>(user);
			}

			var userComment = new UserComment { Comment = comment, User = user };


			user.SavedComments.Add(userComment);
			comment.UsersSaved.Add(userComment);

			UnitOfWork.SaveChanges();

			return Mapper.Map<GetPrivateUserModel>(user);
		}
	}
}
