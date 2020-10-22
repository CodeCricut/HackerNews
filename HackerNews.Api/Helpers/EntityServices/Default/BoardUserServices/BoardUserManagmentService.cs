﻿using AutoMapper;
using CleanEntityArchitecture.Authorization;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.JoinEntities;
using HackerNews.Domain.Models.Board;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public class BoardUserManagmentService : IBoardUserManagementService
	{
		private readonly IMapper _mapper;
		private readonly IReadEntityRepository<Board> _readBoardRepo;
		private readonly IReadEntityRepository<User> _readUserRepo;
		private readonly IUserAuth<User> _cleanUserAuth;

		public BoardUserManagmentService(IMapper mapper,
			IReadEntityRepository<Board> readBoardRepo,
			IReadEntityRepository<User> readUserRepo,
			IUserAuth<User> cleanUserAuth)
		{
			_mapper = mapper;
			_readBoardRepo = readBoardRepo;
			_readUserRepo = readUserRepo;
			_cleanUserAuth = cleanUserAuth;
		}

		public async Task<GetBoardModel> AddBoardModeratorAsync(int boardId, int moderatorId)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			var board = await _readBoardRepo.GetEntityAsync(boardId);
			// verify the current user created the board
			if (board.Creator.Id != currentUser.Id) throw new UnauthorizedException();

			var moderator = await _readUserRepo.GetEntityAsync(moderatorId);
			if (board == null || moderator == null) throw new NotFoundException();

			// remove the moderator if already moderating the board
			var existingModerator = board.Moderators.FirstOrDefault(bu => bu.UserId == moderatorId);
			if (existingModerator != null)
			{
				board.Moderators.Remove(existingModerator);
				moderator.BoardsModerating.Remove(existingModerator);

				await _readBoardRepo.SaveChangesAsync();
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

			await _readBoardRepo.SaveChangesAsync();

			return _mapper.Map<GetBoardModel>(board);
		}

		public async Task<GetBoardModel> AddBoardSubscriberAsync(int boardId)
		{
			var currentUser = await _cleanUserAuth.GetAuthenticatedUserAsync();

			var board = await _readBoardRepo.GetEntityAsync(boardId);
			if (board == null || currentUser == null) throw new NotFoundException();


			// remove if already subbed
			var existingSubscriber = board.Subscribers.FirstOrDefault(s => s.UserId == currentUser.Id);
			if (existingSubscriber != null)
			{
				board.Subscribers.Remove(existingSubscriber);
				currentUser.BoardsSubscribed.Remove(existingSubscriber);

				await _readBoardRepo.SaveChangesAsync();
				return _mapper.Map<GetBoardModel>(board);
			}

			var boardUserSubscriber = new BoardUserSubscriber
			{
				Board = board,
				User = currentUser
			};

			board.Subscribers.Add(boardUserSubscriber);
			currentUser.BoardsSubscribed.Add(boardUserSubscriber);

			await _readBoardRepo.SaveChangesAsync();

			return _mapper.Map<GetBoardModel>(board);
		}
	}
}
