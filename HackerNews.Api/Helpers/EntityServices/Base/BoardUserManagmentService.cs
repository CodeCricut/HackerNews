using AutoMapper;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.JoinEntities;
using HackerNews.Domain.Models.Board;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public abstract class BoardUserManagmentService : IBoardUserManagementService
	{
		private readonly IEntityRepository<Board> _boardRepo;
		private readonly IEntityRepository<User> _userRepo;
		private readonly IMapper _mapper;

		public BoardUserManagmentService(IEntityRepository<Board> boardRepo, IEntityRepository<User> userRepo, IMapper mapper)
		{
			_boardRepo = boardRepo;
			_userRepo = userRepo;
			_mapper = mapper;
		}

		// TODO: refactor
		public virtual async Task<GetBoardModel> AddBoardModeratorAsync(int boardId, User currentUser, int moderatorId)
		{
			var board = await _boardRepo.GetEntityAsync(boardId);
			// verify the current user created the board
			if (board.Creator.Id != currentUser.Id) throw new UnauthorizedException();

			var moderator = await _userRepo.GetEntityAsync(moderatorId);
			if (board == null || moderator == null) throw new NotFoundException();

			// remove the moderator if already moderating the board
			var existingModerator = board.Moderators.FirstOrDefault(bu => bu.UserId == moderatorId);
			if (existingModerator != null)
			{
				board.Moderators.Remove(existingModerator);
				moderator.BoardsModerating.Remove(existingModerator);

				await _boardRepo.SaveChangesAsync();
				return _mapper.Map<GetBoardModel>(board);
			}

			// add the moderator
			var boardUserModerator = new BoardUserModerator
			{
				Board = board,
				User = moderator
			};

			board.Moderators.Add(boardUserModerator);
			moderator.BoardsModerating.Add(boardUserModerator);

			await _boardRepo.SaveChangesAsync();

			return _mapper.Map<GetBoardModel>(board);
		}


		public virtual async Task<GetBoardModel> AddBoardSubscriberAsync(int boardId, User currentUser)
		{
			var board = await _boardRepo.GetEntityAsync(boardId);
			if (board == null || currentUser == null) throw new NotFoundException();


			// remove if already subbed
			var existingSubscriber = board.Subscribers.FirstOrDefault(s => s.UserId == currentUser.Id);
			if (existingSubscriber != null)
			{
				board.Subscribers.Remove(existingSubscriber);
				currentUser.BoardsSubscribed.Remove(existingSubscriber);

				await _boardRepo.SaveChangesAsync();
				return _mapper.Map<GetBoardModel>(board);
			}

			var boardUserSubscriber = new BoardUserSubscriber
			{
				Board = board,
				User = currentUser
			};

			board.Subscribers.Add(boardUserSubscriber);
			currentUser.BoardsSubscribed.Add(boardUserSubscriber);

			await _boardRepo.SaveChangesAsync();

			return _mapper.Map < GetBoardModel > (board);
		}
	}
}
