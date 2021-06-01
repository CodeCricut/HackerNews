using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.AddBoard
{
	public class AddBoardCommand : IRequest<GetBoardModel>
	{
		public AddBoardCommand(PostBoardModel postBoardModel)
		{
			PostBoardModel = postBoardModel;
		}

		public PostBoardModel PostBoardModel { get; }
	}

	public class AddBoardHandler : DatabaseRequestHandler<AddBoardCommand, GetBoardModel>
	{
		public AddBoardHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(AddBoardCommand request, CancellationToken cancellationToken)
		{
			User user = await GetCurrentUser();

			await VerifyBoardTitleNotTaken(request.PostBoardModel.Title);

			Board board = CreateBoardFromModel(request.PostBoardModel, user);
			AddUserAsBoardCreator(user, board);
			AddUserAsModerator(user, board);

			Board addedBoard = await AddBoard(board);

			return MapBoardToModel(addedBoard);
		}

		private async Task<User> GetCurrentUser()
		{
			if (!await IsLoggedIn())
				throw new UnauthorizedException();
			return await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);
		}

		private Task<bool> IsLoggedIn()
		{
			return UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId); 
		}

		private async Task VerifyBoardTitleNotTaken(string boardTitle)
		{
			var boards = await UnitOfWork.Boards.GetEntitiesAsync();
			var boardWithSameTitle = boards.FirstOrDefault(board => board.Title == boardTitle);
			if (boardWithSameTitle != null) throw new BoardTitleTakenException();
		}

		private void AddUserAsModerator(User user, Board board)
		{
			var userBoardModerator = new BoardUserModerator
			{
				Board = board,
				User = user
			};
			board.Moderators.Add(userBoardModerator);
		}

		private Board CreateBoardFromModel(PostBoardModel boardModel, User user)
		{
			var board = Mapper.Map<Board>(boardModel);
			board.CreateDate = DateTime.Now;
			return board;
		}

		private void AddUserAsBoardCreator(User user, Board board)
		{
			board.Creator = user;
		}
		private async Task<Board> AddBoard(Board board)
		{
			var addedBoard = await UnitOfWork.Boards.AddEntityAsync(board);
			UnitOfWork.SaveChanges();
			return addedBoard;
		}

		private GetBoardModel MapBoardToModel(Board addedBoard)
		{
			return Mapper.Map<GetBoardModel>(addedBoard);
		}
	}
}
