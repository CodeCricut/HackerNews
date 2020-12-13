using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.AddImage
{
	public class AddUserImageCommand : IRequest<GetPublicUserModel>
	{
		public AddUserImageCommand(PostImageModel imageModel, int currentUserId)
		{
			ImageModel = imageModel;
			CurrentUserId = currentUserId;
		}

		public PostImageModel ImageModel { get; }
		public int CurrentUserId { get; }
	}

	public class AddUserImageHandler : DatabaseRequestHandler<AddUserImageCommand, GetPublicUserModel>
	{
		public AddUserImageHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetPublicUserModel> Handle(AddUserImageCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(request.CurrentUserId))
				throw new UnauthorizedException();
			var user = await UnitOfWork.Users.GetEntityAsync(request.CurrentUserId);

			// Create image
			var imageToAdd = Mapper.Map<Image>(request.ImageModel);
			imageToAdd.UserId = _currentUserService.UserId;

			await UnitOfWork.Images.AddEntityAsync(imageToAdd);

			user.ProfileImage = imageToAdd;

			// Save
			UnitOfWork.SaveChanges();

			// Return updated board
			return Mapper.Map<GetPublicUserModel>(await UnitOfWork.Users.GetEntityAsync(request.CurrentUserId));
		}
	}
}
