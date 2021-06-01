using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
			await VerifyLoggedIn();

			Board board = await GetBoardById(request.BoardId);

			VerifyUserCreatedBoard(board);

			var imageToAdd = MapModelToImage(request.ImageModel);

			await AddImage(imageToAdd, board);

			UnitOfWork.SaveChanges();

			return MapBoardToModel(board);
		}

		private async Task VerifyLoggedIn()
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId))
				throw new UnauthorizedException();
		}

		private async Task<Board> GetBoardById(int boardId)
		{
			Board board = await UnitOfWork.Boards.GetEntityAsync(boardId);
			if (board == null) throw new NotFoundException();
			return board;
		}

		private void VerifyUserCreatedBoard(Board board)
		{
			if (board.Creator.Id != _currentUserService.UserId) throw new UnauthorizedException();
		}

		private Image MapModelToImage(PostImageModel imageModel)
		{
			return Mapper.Map<Image>(imageModel);
		}

		private async Task AddImage(Image imageToAdd, Board board)
		{
			imageToAdd.BoardId = board.Id;

			await UnitOfWork.Images.AddEntityAsync(imageToAdd);

			// Add image to board
			board.BoardImage = imageToAdd;
		}

		private GetBoardModel MapBoardToModel(Board board)
		{

			// Return updated board
			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
