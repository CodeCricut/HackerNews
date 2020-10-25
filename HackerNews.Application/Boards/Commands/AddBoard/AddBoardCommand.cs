using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
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
		private readonly ICurrentUserService _currentUserService;

		public AddBoardHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetBoardModel> Handle(AddBoardCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);
				if (user == null) throw new UnauthorizedException();

				var board = Mapper.Map<Board>(request.PostBoardModel);
				board.CreateDate = DateTime.Now;
				board.Creator = user;

				// Add user to moderators
				var userBoardModerator = new BoardUserModerator
				{
					Board = board,
					User = user
				};
				board.Moderators.Add(userBoardModerator);

				var addedBoard = await UnitOfWork.Boards.AddEntityAsync(board);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetBoardModel>(addedBoard);
			}
		}
	}
}
