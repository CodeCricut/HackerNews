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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.AddBoards
{
	public class AddBoardsCommand : IRequest<IEnumerable<GetBoardModel>>
	{
		public AddBoardsCommand(IEnumerable<PostBoardModel> postBoardModels)
		{
			PostBoardModels = postBoardModels;
		}

		public IEnumerable<PostBoardModel> PostBoardModels { get; }
	}

	public class AddBoardsHandler : DatabaseRequestHandler<AddBoardsCommand, IEnumerable<GetBoardModel>>
	{
		public AddBoardsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<IEnumerable<GetBoardModel>> Handle(AddBoardsCommand request, CancellationToken cancellationToken)
		{
			IEnumerable<Board> boards = MapModelsToBoards(request.PostBoardModels);

			User user = await GetUser();

			foreach (var board in boards)
				ApplySpecialBoardProperties(board, user);

			IEnumerable<Board> addedBoards = await AddBoards(boards);

			return MapBoardsToModels(addedBoards);
		}

		private IEnumerable<Board> MapModelsToBoards(IEnumerable<PostBoardModel> models)
		{
			return Mapper.Map<IEnumerable<Board>>(models);
		}

		private void ApplySpecialBoardProperties(Board board, User user)
		{
			ApplyCreateDateAsNow(board);
			AddUserAsCreator(user, board);
			AddUserAsBoardModerator(user, board);
		}

		private async Task<User> GetUser()
		{
			var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);
			if (user == null) throw new UnauthorizedException();
			return user;
		}

		private static void ApplyCreateDateAsNow(Board board)
		{
			board.CreateDate = DateTime.Now;
		}

		private static void AddUserAsCreator(User user, Board board)
		{
			board.Creator = user;
		}

		private static void AddUserAsBoardModerator(User user, Board board)
		{
			var boardUser = new BoardUserModerator
			{
				Board = board,
				User = user
			};
			board.Moderators.Add(boardUser);
		}

		private async Task<IEnumerable<Board>> AddBoards(IEnumerable<Board> boards)
		{
			var addedBoards = await UnitOfWork.Boards.AddEntititesAsync(boards);
			UnitOfWork.SaveChanges();
			return addedBoards;
		}

		private IEnumerable<GetBoardModel> MapBoardsToModels(IEnumerable<Board> addedBoards)
		{
			return Mapper.Map<IEnumerable<GetBoardModel>>(addedBoards);
		}
	}
}
