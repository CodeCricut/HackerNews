using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.AddImage
{
	public class AddBoardImageCommand : IRequest<GetBoardModel>
	{
		public AddBoardImageCommand(PostImageModel imageModel, int boardId)
		{
			ImageModel = imageModel;
			BoardId = boardId;
		}

		public PostImageModel ImageModel { get; }
		public int BoardId { get; }
	}

	public class AddBoardImageHandler : DatabaseRequestHandler<AddBoardImageCommand, GetBoardModel>
	{
		public AddBoardImageHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(AddBoardImageCommand request, CancellationToken cancellationToken)
		{
			// Verify logged in
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId))
				throw new UnauthorizedException();

			// Verify board exists
			Board board = await UnitOfWork.Boards.GetEntityAsync(request.BoardId);
			if (board == null) throw new NotFoundException();

			// Verify user created board
			if (board.Creator.Id != _currentUserService.UserId) throw new UnauthorizedException();

			// Create image
			var imageToAdd = Mapper.Map<Image>(request.ImageModel);
			imageToAdd.BoardId = board.Id;

			await UnitOfWork.Images.AddEntityAsync(imageToAdd);

			// Add image to board
			board.BoardImage = imageToAdd;

			// Save
			UnitOfWork.SaveChanges();

			// Return updated board
			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
