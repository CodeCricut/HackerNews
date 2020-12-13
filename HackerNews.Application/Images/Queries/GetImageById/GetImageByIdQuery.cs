using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Images.Queries.GetImageById
{
	public class GetImageByIdQuery : IRequest<GetImageModel>
	{
		public GetImageByIdQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetImageByIdHandler : DatabaseRequestHandler<GetImageByIdQuery, GetImageModel>
	{
		public GetImageByIdHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetImageModel> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
		{
			var image = await UnitOfWork.Images.GetEntityAsync(request.Id);
			if (image == null) throw new NotFoundException();
			return Mapper.Map<GetImageModel>(image);
		}
	}
}
